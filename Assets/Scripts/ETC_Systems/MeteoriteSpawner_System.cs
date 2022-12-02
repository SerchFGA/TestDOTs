using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Collections;

public partial class MeteoriteSpawner_System : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery meteoriteEntityQuery = EntityManager.CreateEntityQuery(typeof(MeteoriteTag));
        var spawnerEntity = SystemAPI.GetSingletonEntity<MeteoriteSpawner_Component>();


        MeteoriteSpawner_Component meteoriteSpawner_Component = SystemAPI.GetSingleton<MeteoriteSpawner_Component>();
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        MeteoriteSpawner_Aspect meteorite = SystemAPI.GetAspectRW<MeteoriteSpawner_Aspect>(spawnerEntity);

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);


        meteorite.MeteoriteSpawnTimer -= SystemAPI.Time.DeltaTime;
        if (!meteorite.TimeToSpawnMeteorites) 
            return;
        else
        {
            meteorite.MeteoriteSpawnTimer = meteorite.meteoriteSpawnRate;
            Entity spawnedEntity = entityCommandBuffer.Instantiate(meteorite.meteoritePrefab);

            entityCommandBuffer.SetComponent(spawnedEntity, new Translation
            {
                Value = meteorite.GetRandomPos(randomComponent)
            });

            entityCommandBuffer.SetComponent(spawnedEntity, new TargetMovement
            {
                value = new Unity.Mathematics.float3(randomComponent.ValueRW.random.NextFloat(-2, 2), 0, randomComponent.ValueRW.random.NextFloat(-2, 2))
            });  
        }


       
    }





}
