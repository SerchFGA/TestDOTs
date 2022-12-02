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

    public GameObject s_meteoritePrefab;
    public bool isSpawningAgain;
    public float3 posSpawningAgain;


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
            s_meteoritePrefab = GetEntity(authoring.s_meteoritePrefab),

        });

        AddComponent(new secondSpawner_Component
        {
            isSpawning2ndTime = authoring.isSpawningAgain,
            posSpawningAgain = authoring.posSpawningAgain,

        });

        AddComponent<MeteoriteSpawnTimer>();
    }
}
