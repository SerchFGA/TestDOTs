using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EnemySpawner_Component : IComponentData
{
    public Entity enemyPrefab;
    public float enemySpawnRate;

}

public struct EnemySpawnTimer_Component : IComponentData
{
    public float value;
}
