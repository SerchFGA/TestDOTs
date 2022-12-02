using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Aspects;
using UnityEngine;

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
        new ShootBulletMove
        {
            deltaTime = deltaTimeBullet
        }.ScheduleParallel();

        var ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        SimulationSingleton simulation = SystemAPI.GetSingleton<SimulationSingleton>();

        state.Dependency = new BulletTriggerHitJob()
        {
            Bullet = SystemAPI.GetComponentLookup<BulletTag>(false),
            Border = SystemAPI.GetComponentLookup<BorderTag>(false),
            ECB = ECB
        }.Schedule(simulation, state.Dependency);

    }
}

[BurstCompile]
public partial struct ShootBulletMove : IJobEntity
{
    public float deltaTime;
    public void Execute(Bullet_Aspect bullet_Aspect)
    {
        bullet_Aspect.ShootBullet(deltaTime);
    }

}

[BurstCompile]
public partial struct BulletTriggerHitJob : ITriggerEventsJob
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


        if (!Entity.Null.Equals(border) && !Entity.Null.Equals(bullet))
        {
            //UnityEngine.Debug.Log("Choco Asteroide GAME OVER");
            ECB.DestroyEntity(bullet);

        }


    }
}


