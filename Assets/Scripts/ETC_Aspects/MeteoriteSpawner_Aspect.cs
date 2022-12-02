using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public readonly partial struct MeteoriteSpawner_Aspect : IAspect
{
    public readonly Entity entity;

    private readonly TransformAspect _transformAspect;

    private readonly RefRO<MeteoriteSpawner_Component> _meteoriteSpawner;
    private readonly RefRW<secondSpawner_Component> _seconSpawner;
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

    public Entity s_meteoritePrefab => _meteoriteSpawner.ValueRO.s_meteoritePrefab;
    public bool isSpawningAgain
    {
        get => _seconSpawner.ValueRO.isSpawning2ndTime;
        set => _seconSpawner.ValueRW.isSpawning2ndTime = value;
    }

    public float3 posSpawningAgain
    {
        get => _seconSpawner.ValueRO.posSpawningAgain;
        set => _seconSpawner.ValueRW.posSpawningAgain = value;
    }

}
