using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Scenes;

namespace SFGA.Test
{
    public partial class PlayerShooter_System : SystemBase
    {
        
        protected override void OnUpdate()
        {
            //Get all bullets
            EntityQuery bulletsEntityQuery = EntityManager.CreateEntityQuery(typeof(BulletTag));

            //Get player Entity
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

            //Get Aspect from Entity
            var playerShooter = SystemAPI.GetAspectRW<PlayerShooter_Aspect>(playerEntity);
            var playerTransform = SystemAPI.GetAspectRW<TransformAspect>(playerEntity);

            EntityCommandBuffer ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            //Shoot rate
            playerShooter.PlayerShooterTimer -= SystemAPI.Time.DeltaTime;

            //Check when to shoot
            if (Input.GetKey(KeyCode.Space) && playerShooter.TimeToShoot)
            {
                playerShooter.PlayerShooterTimer = playerShooter.shootRate;

                //Instantiate Bullet
                Entity spawnBullet = ECB.Instantiate(playerShooter.bulletPrefab);

                //Play sound
                GameManager_Script.Instance.shooting();

                var spawnPointTransform = SystemAPI.GetAspectRW<TransformAspect>(playerShooter.spawnPointEntity);
                Vector3 directionBullet = (playerTransform.Position - spawnPointTransform.Position);
                float3 dir = directionBullet.normalized;

                //Set Bullet components values
                ECB.SetComponent(spawnBullet, new Bullet_Component
                {
                    bulletDir = -dir
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
}

