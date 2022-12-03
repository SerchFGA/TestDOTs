using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SFGA.Test
{
    public readonly partial struct EnemySpawner_Aspect : IAspect
    {
        public readonly Entity entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<EnemySpawner_Component> _component;
        private readonly RefRW<EnemySpawnTimer_Component> _timer;

        

        public float EnemySpawnTimer
        {
            get => _timer.ValueRO.value;
            set => _timer.ValueRW.value = value;
        }

        public bool TimeToSpawnEnemys => EnemySpawnTimer <= 0;

        public float enemySpawnRate => _component.ValueRO.enemySpawnRate;

        public Entity enemyPrefab => _component.ValueRO.enemyPrefab;

        //Get random Spawning Position
        public float3 GetRandomPos(RefRW<RandomComponent> randomComponent)
        {
            float3 randomposition;
            int XStart = 25;
            int zRange = 10;

            randomposition = new float3(
                     XStart,
                     0,
                     randomComponent.ValueRW.random.NextFloat(-zRange, zRange));



            return randomposition;
        }

        
    }
}

