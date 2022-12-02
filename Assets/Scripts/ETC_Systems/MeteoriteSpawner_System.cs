using Unity.Entities;
using Unity.Transforms;


namespace SFGA.Test
{
    //Meteorite Spawner System
    public partial class MeteoriteSpawner_System : SystemBase
    {
        protected override void OnUpdate()
        {
            //Create query for metorites
            EntityQuery meteoriteEntityQuery = EntityManager.CreateEntityQuery(typeof(MeteoriteTag));
            var spawnerEntity = SystemAPI.GetSingletonEntity<MeteoriteSpawner_Component>();

            RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

            MeteoriteSpawner_Aspect meteorite = SystemAPI.GetAspectRW<MeteoriteSpawner_Aspect>(spawnerEntity);

            EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            //Check spawn time
            meteorite.MeteoriteSpawnTimer -= SystemAPI.Time.DeltaTime;
            if (!meteorite.TimeToSpawnMeteorites)
                return;
            else
            {
                meteorite.MeteoriteSpawnTimer = meteorite.meteoriteSpawnRate;
                Entity spawnedEntity = entityCommandBuffer.Instantiate(meteorite.meteoritePrefab);

                //Set components value from spawnedEntity
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
}

