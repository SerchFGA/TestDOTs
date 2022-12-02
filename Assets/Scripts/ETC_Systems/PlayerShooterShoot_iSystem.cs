using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace SFGA.Test
{

    [BurstCompile]
    public partial struct PlayerShooterShoot_iSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

            float deltaTimeBullet = SystemAPI.Time.DeltaTime;

            //Job to move Bullet
            new ShootBulletMove
            {
                deltaTime = deltaTimeBullet
            }.ScheduleParallel();

            var ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            SimulationSingleton simulation = SystemAPI.GetSingleton<SimulationSingleton>();

            //Job to detect collisions
            state.Dependency = new BulletTriggerHitJob()
            {
                Bullet = SystemAPI.GetComponentLookup<BulletTag>(false),
                Border = SystemAPI.GetComponentLookup<BorderTag>(false),
                ECB = ECB
            }.Schedule(simulation, state.Dependency);

        }
    }

    
    [BurstCompile]
    public partial struct ShootBulletMove : IJobEntity                      //Job to move Bullet
    {
        public float deltaTime;
        public void Execute(Bullet_Aspect bullet_Aspect)
        {
            bullet_Aspect.ShootBullet(deltaTime);
        }

    }

    [BurstCompile]
    public partial struct BulletTriggerHitJob : ITriggerEventsJob           //Job to detect collisions with borders and destroy bullets
    {
        public ComponentLookup<BulletTag> Bullet;
        public ComponentLookup<BorderTag> Border;

        public EntityCommandBuffer ECB;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity border = Entity.Null;
            Entity bullet = Entity.Null;


            if (Border.HasComponent(triggerEvent.EntityA))
                border = triggerEvent.EntityA;
            if (Border.HasComponent(triggerEvent.EntityB))
                border = triggerEvent.EntityB;

            if (Bullet.HasComponent(triggerEvent.EntityA))
                bullet = triggerEvent.EntityA;
            if (Bullet.HasComponent(triggerEvent.EntityB))
                bullet = triggerEvent.EntityB;

            //Check Collision Entity
            if (!Entity.Null.Equals(border) && !Entity.Null.Equals(bullet))
            {
                ECB.DestroyEntity(bullet);
            }


        }
    }
}



