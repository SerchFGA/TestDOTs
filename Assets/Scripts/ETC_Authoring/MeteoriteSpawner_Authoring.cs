using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class MeteoriteSpawner_Authoring : MonoBehaviour
{

    public GameObject meteoritePrefab;
    public float meteoriteSpawnerRate;
    public float meteoriteSpanerTimer;

    public float xLimit;
    public float zLimit;

    public GameObject m_meteoritePrefab;
    public bool isSpawning2ndAgain;
    public float3 posSpawn2ndAgain;

    public GameObject s_meteoritePrefab;
    public bool isSpawning3ndAgain;
    public float3 posSpawn3ThAgain;
}

public class MeteoriteSpawnerBaker : Baker<MeteoriteSpawner_Authoring>
{
    public override void Bake(MeteoriteSpawner_Authoring authoring)
    {
        AddComponent(new MeteoriteSpawner_Component
        {
            metoritePrefab = GetEntity(authoring.meteoritePrefab),
            meteoriteSpawnRate = authoring.meteoriteSpawnerRate,
            xLimit = authoring.xLimit,
            zLimit = authoring.zLimit,
            m_meteoritePrefab = GetEntity(authoring.m_meteoritePrefab),
            s_meteoritePrefab = GetEntity(authoring.s_meteoritePrefab),
        });

        AddComponent(new secondSpawner_Component
        {
            isSpawning2ndTime = authoring.isSpawning2ndAgain,
            posSpawning2bdAgain = authoring.posSpawn2ndAgain,

        });

        AddComponent(new thirdSpawner_Component
        {
            isSpawning3thTime = authoring.isSpawning3ndAgain,
            posSpawning3thAgain = authoring.posSpawn3ThAgain,
        });

        AddComponent<MeteoriteSpawnTimer>();
    }
}
