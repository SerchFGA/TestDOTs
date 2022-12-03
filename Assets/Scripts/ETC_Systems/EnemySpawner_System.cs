using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SFGA.Test
{
    //Enemy Spawner System
    public partial class EnemySpawner_System : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityQuery EnemyEntityQuery = EntityManager.CreateEntityQuery(typeof(EnemyTag_Component));
            var spawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawner_Component>();

            RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

            EnemySpawner_Aspect enemyAspect = SystemAPI.GetAspectRW<EnemySpawner_Aspect>(spawnerEntity);
            EntityCommandBuffer ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            //Check spawn time
            enemyAspect.EnemySpawnTimer -= SystemAPI.Time.DeltaTime;
            if (!enemyAspect.TimeToSpawnEnemys)
                return;
            else
            {
                enemyAspect.EnemySpawnTimer = enemyAspect.enemySpawnRate;
                Entity spawnEntity = ECB.Instantiate(enemyAspect.enemyPrefab);

                //Set components value from spawnedEntity
                ECB.SetComponent(spawnEntity, new Translation
                {
                    Value = enemyAspect.GetRandomPos(randomComponent)
                });

                ECB.SetComponent(spawnEntity, new TargetMovement
                {
                    value = new float3(-1, 0, 0)
                });
            }
        }
        
    }
}

