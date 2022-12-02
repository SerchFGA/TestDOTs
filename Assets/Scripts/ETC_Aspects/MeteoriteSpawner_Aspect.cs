using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace SFGA.Test
{
    public readonly partial struct MeteoriteSpawner_Aspect : IAspect
    {
        public readonly Entity entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<MeteoriteSpawner_Component> _meteoriteSpawner;
        private readonly RefRW<secondSpawner_Component> _seconSpawner;
        private readonly RefRW<thirdSpawner_Component> _thirdSpawner;
        private readonly RefRW<MeteoriteSpawnTimer> _meteoriteSpawnTimer;

        public float MeteoriteSpawnTimer
        {
            get => _meteoriteSpawnTimer.ValueRO.Value;
            set => _meteoriteSpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnMeteorites => MeteoriteSpawnTimer <= 0;

        public float meteoriteSpawnRate => _meteoriteSpawner.ValueRO.meteoriteSpawnRate;

        public Entity meteoritePrefab => _meteoriteSpawner.ValueRO.metoritePrefab;


        private float xLimits => _meteoriteSpawner.ValueRO.xLimit;
        private float zLimits => _meteoriteSpawner.ValueRO.zLimit;

        //Get random Spawning Position
        public float3 GetRandomPos(RefRW<RandomComponent> randomComponent)
        {
            float3 randomposition;
            int xRange = 20;
            int zRange = 10;

            do
            {
                randomposition = new float3(randomComponent.ValueRW.random.NextFloat(-xLimits, xLimits), 0, randomComponent.ValueRW.random.NextFloat(-zLimits, zLimits));
            } while (randomposition.x < xRange && randomposition.x > -xRange && randomposition.z < zRange && randomposition.z > -zRange);

            return randomposition;
        }

        // Double Spawning meteorites

        public Entity m_meteoritePrefab => _meteoriteSpawner.ValueRO.m_meteoritePrefab;
        public bool isSpawn2ndTime
        {
            get => _seconSpawner.ValueRO.isSpawning2ndTime;
            set => _seconSpawner.ValueRW.isSpawning2ndTime = value;
        }

        public float3 posSpawning2ndAgain
        {
            get => _seconSpawner.ValueRO.posSpawning2bdAgain;
            set => _seconSpawner.ValueRW.posSpawning2bdAgain = value;
        }

        // Triple Spawning meteorites
        public Entity s_meteoritePrefab => _meteoriteSpawner.ValueRO.s_meteoritePrefab;
        public bool isSpawn3thTime
        {
            get => _thirdSpawner.ValueRO.isSpawning3thTime;
            set => _thirdSpawner.ValueRW.isSpawning3thTime = value;
        }

        public float3 posSpawning3tnAgain
        {
            get => _thirdSpawner.ValueRO.posSpawning3thAgain;
            set => _thirdSpawner.ValueRW.posSpawning3thAgain = value;
        }
    }
}

