using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct MeteoriteSpawner_Component : IComponentData
{
    public Entity metoritePrefab;
    public float meteoriteSpawnRate;

    public float xLimit;
    public float zLimit;


    public Entity s_meteoritePrefab;

}

public struct secondSpawner_Component : IComponentData
{
    
    public bool isSpawningAgain;
    public float3 posSpawningAgain;
}

public struct MeteoriteSpawnTimer : IComponentData
{
    public float Value;
}