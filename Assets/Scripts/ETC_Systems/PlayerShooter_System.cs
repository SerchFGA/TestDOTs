using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics.Aspects;
using Unity.Transforms;

public partial class PlayerShooter_System : SystemBase
{
    

    protected override void OnCreate()
    {
        base.OnCreate();
        
    }
    protected override void OnUpdate()
    {
        EntityQuery bulletsEntityQuery = EntityManager.CreateEntityQuery(typeof(BulletTag));

        
        var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        //var bulletEntity = SystemAPI.GetSingletonEntity<BulletTag>();
        //var bullet = SystemAPI.GetAspectRW<Bullet_Aspect>(bulletEntity);

        var playerShooter = SystemAPI.GetAspectRW<PlayerShooter_Aspect>(playerEntity);
        var playerTransform = SystemAPI.GetAspectRW<TransformAspect>(playerEntity);
        

        EntityCommandBuffer ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

        //Shoot
        playerShooter.PlayerShooterTimer -= SystemAPI.Time.DeltaTime;

        if (Input.GetKey(KeyCode.Space) && playerShooter.TimeToShoot)
        {
            
            playerShooter.PlayerShooterTimer = playerShooter.shootRate;
            Entity spawnBullet = ECB.Instantiate(playerShooter.bulletPrefab);

            GameManager_Script.Instance.shooting();

            var spawnPointTransform = SystemAPI.GetAspectRW<TransformAspect>(playerShooter.spawnPointEntity);
            

            Vector3 directionBullet = (playerTransform.Position - spawnPointTransform.Position);
            float3 dir = directionBullet.normalized;

            ECB.SetComponent(spawnBullet, new Bullet_Component
            {
                bulletDir= -dir
            });

            ECB.SetComponent(spawnBullet, new Translation
            {
                Value = spawnPointTransform.Position
            });

            ECB.SetComponent(spawnBullet, new Rotation
            {
                Value = spawnPointTransform.Rotation
            });

            
        }

    }
}
