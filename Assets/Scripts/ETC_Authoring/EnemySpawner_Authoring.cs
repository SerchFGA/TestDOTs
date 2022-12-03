using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawner_Authoring : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnRate;

    public float timer;
}

public class EnemySpawnerBaker : Baker<EnemySpawner_Authoring>
{
    public override void Bake(EnemySpawner_Authoring authoring)
    {
        AddComponent(new EnemySpawner_Component
        {
            enemyPrefab = GetEntity(authoring.enemyPrefab),
            enemySpawnRate = authoring.enemySpawnRate,
        });

        AddComponent(new EnemySpawnTimer_Component
        {
            value = authoring.timer,
        });

    }
}
