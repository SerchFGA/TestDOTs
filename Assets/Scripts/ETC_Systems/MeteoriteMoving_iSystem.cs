using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Physics;
using Unity.Collections;
using Unity.Transforms;

namespace SFGA.Test
{
    [BurstCompile]
    public partial struct MeteoriteMoving_iSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Call job to move meteorites
            float deltaTime = SystemAPI.Time.DeltaTime;
            new MoveMeteorite
            {
                deltaTime = deltaTime
            }.ScheduleParallel();


            var ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            SimulationSingleton simulation = SystemAPI.GetSingleton<SimulationSingleton>();



            //Creat job to detect collision with meteorites/player/bullets
            var job = new BulletHitJob()
            {
                Bullet = SystemAPI.GetComponentLookup<BulletTag>(),
                Meteorite = SystemAPI.GetComponentLookup<MeteoriteTag>(false),
                MediumMet = SystemAPI.GetComponentLookup<MediumMetTag>(false),
                SmallMet = SystemAPI.GetComponentLookup<SmallMetTag>(false),
                Player = SystemAPI.GetComponentLookup<PlayerTag>(false),
                ECB = ECB,
                shouldAddPoints = new NativeReference<bool>(Allocator.TempJob),
                shouldAddDoublePoints = new NativeReference<bool>(Allocator.TempJob),
                shouldAddTriplePoints = new NativeReference<bool>(Allocator.TempJob),
                shouldRemoveLives = new NativeReference<bool>(Allocator.TempJob),
                posMeteorite = new NativeReference<Entity>(Allocator.TempJob),

            };
            job.Schedule(simulation, state.Dependency).Complete();

            var AddPoints = job.shouldAddPoints.Value;
            var AddDoublePoints = job.shouldAddDoublePoints.Value;
            var AddTriplePoints = job.shouldAddDoublePoints.Value;
            var RemoveLives = job.shouldRemoveLives.Value;
            var posMet = job.posMeteorite.Value;
            job.shouldAddPoints.Dispose();
            job.shouldAddDoublePoints.Dispose();
            job.shouldAddTriplePoints.Dispose();
            job.shouldRemoveLives.Dispose();
            job.posMeteorite.Dispose();


            var spawnerEntity = SystemAPI.GetSingletonEntity<MeteoriteSpawner_Component>();
            if (AddPoints)
            {
                ECB.SetComponent(spawnerEntity, new secondSpawner_Component
                {
                    posSpawning2bdAgain = SystemAPI.GetAspectRW<TransformAspect>(posMet).Position,
                    isSpawning2ndTime = true,
                });

                GameManager_Script.Instance.addPoints(1);
            }

            if (AddDoublePoints)
            {
                ECB.SetComponent(spawnerEntity, new thirdSpawner_Component
                {
                    posSpawning3thAgain = SystemAPI.GetAspectRW<TransformAspect>(posMet).Position,
                    isSpawning3thTime = true,
                });
                GameManager_Script.Instance.addPoints(2);
            }

            if (AddTriplePoints)
                GameManager_Script.Instance.addPoints(3);


            if (RemoveLives)
                GameManager_Script.Instance.SpaceShipDestroy();
        }
    }

    // Job to Move Meteorites
    [BurstCompile]
    public partial struct MoveMeteorite : IJobEntity
    {
        public float deltaTime;
        public void Execute(Movemeteorite_Aspect movemeteorite_Aspect)
        {
            movemeteorite_Aspect.Move(deltaTime);
        }
    }

    [BurstCompile]
    public partial struct BulletHitJob : ITriggerEventsJob
    {
        public ComponentLookup<BulletTag> Bullet;
        public ComponentLookup<MeteoriteTag> Meteorite;
        public ComponentLookup<MediumMetTag> MediumMet;
        public ComponentLookup<SmallMetTag> SmallMet;
        public ComponentLookup<PlayerTag> Player;

        public EntityCommandBuffer ECB;

        public NativeReference<bool> shouldAddPoints;
        public NativeReference<bool> shouldAddDoublePoints;
        public NativeReference<bool> shouldAddTriplePoints;
        public NativeReference<bool> shouldRemoveLives;

        public NativeReference<Entity> posMeteorite;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity meteorite = Entity.Null;
            Entity mediumMet = Entity.Null;
            Entity smallMet = Entity.Null;
            Entity player = Entity.Null;
            Entity bullet = Entity.Null;


            if (Player.HasComponent(triggerEvent.EntityA))
                player = triggerEvent.EntityA;
            if (Player.HasComponent(triggerEvent.EntityB))
                player = triggerEvent.EntityB;

            if (Bullet.HasComponent(triggerEvent.EntityA))
                bullet = triggerEvent.EntityA;
            if (Bullet.HasComponent(triggerEvent.EntityB))
                bullet = triggerEvent.EntityB;

            if (Meteorite.HasComponent(triggerEvent.EntityA))
                meteorite = triggerEvent.EntityA;
            if (Meteorite.HasComponent(triggerEvent.EntityB))
                meteorite = triggerEvent.EntityB;

            if (MediumMet.HasComponent(triggerEvent.EntityA))
                mediumMet = triggerEvent.EntityA;
            if (MediumMet.HasComponent(triggerEvent.EntityB))
                mediumMet = triggerEvent.EntityB;

            if (SmallMet.HasComponent(triggerEvent.EntityA))
                smallMet = triggerEvent.EntityA;
            if (SmallMet.HasComponent(triggerEvent.EntityB))
                smallMet = triggerEvent.EntityB;


            //Collision meteorites vs player
            if (!Entity.Null.Equals(meteorite) && !Entity.Null.Equals(player))
            {
                UnityEngine.Debug.Log("Choco Asteroide -1 Vida");
                shouldRemoveLives.Value = true;
                DesEntity(meteorite);
            }

            if (!Entity.Null.Equals(mediumMet) && !Entity.Null.Equals(player))
            {
                UnityEngine.Debug.Log("Choco Asteroide -1 Vida");
                shouldRemoveLives.Value = true;
                DesEntity(mediumMet);
            }

            if (!Entity.Null.Equals(smallMet) && !Entity.Null.Equals(player))
            {
                UnityEngine.Debug.Log("Choco Asteroide -1 Vida");
                shouldRemoveLives.Value = true;
                DesEntity(smallMet);
            }

            //Collision meteorites vs bullets
            if (!Entity.Null.Equals(meteorite) && !Entity.Null.Equals(bullet))
            {
                UnityEngine.Debug.Log("Destruyo Asteroide");
                shouldAddPoints.Value = true;
                posMeteorite.Value = meteorite;
                DesEntity(meteorite);
                DesEntity(bullet);
            }

            if (!Entity.Null.Equals(mediumMet) && !Entity.Null.Equals(bullet))
            {
                UnityEngine.Debug.Log("Destruyo Asteroide mediano");
                shouldAddTriplePoints.Value = true;
                posMeteorite.Value = mediumMet;
                DesEntity(mediumMet);
                DesEntity(bullet);
            }

            if (!Entity.Null.Equals(smallMet) && !Entity.Null.Equals(bullet))
            {
                UnityEngine.Debug.Log("Destruyo Asteroide pequeño");
                shouldAddDoublePoints.Value = true;
                DesEntity(smallMet);
                DesEntity(bullet);
            }

        }

        //Destroy Entities method
        private void DesEntity(Entity entity)
        {
            ECB.DestroyEntity(entity);
        }

    }
}




