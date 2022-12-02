using Unity.Entities;
using Unity.Transforms;

namespace SFGA.Test
{
    //Meteorite Spawner System
    public partial class MeteoriteSecondSpawner_System : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityQuery meteoriteEntityQuerySecond = EntityManager.CreateEntityQuery(typeof(MeteoriteTag));
            var spawnerEntity = SystemAPI.GetSingletonEntity<MeteoriteSpawner_Component>();

            RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
            MeteoriteSpawner_Aspect meteorite = SystemAPI.GetAspectRW<MeteoriteSpawner_Aspect>(spawnerEntity);

            EntityCommandBuffer ETC2 = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            //Check spawn time
            meteorite.MeteoriteSpawnTimer -= SystemAPI.Time.DeltaTime;
            if (!meteorite.isSpawningAgain)
                return;
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Entity spawnedEntity = ETC2.Instantiate(meteorite.s_meteoritePrefab);

                    //Set components value from spawnedEntity
                    ETC2.SetComponent(spawnedEntity, new Translation
                    {
                        Value = meteorite.posSpawningAgain
                    });

                    ETC2.SetComponent(spawnedEntity, new TargetMovement
                    {
                        value = new Unity.Mathematics.float3(randomComponent.ValueRW.random.NextFloat(-2, 2), 0, randomComponent.ValueRW.random.NextFloat(-2, 2))
                    });

                }
                meteorite.isSpawningAgain = false;
            }

        }
    }
}

