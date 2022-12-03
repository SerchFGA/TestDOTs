using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Authoring : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float3 shootingTarget;

    public float shootingRate;
    public float shootingTimer;
}

public class EnemyBaker : Baker<Enemy_Authoring>
{
    public override void Bake(Enemy_Authoring authoring)
    {
        AddComponent(new Enemy_Component
        {
            bulletPrefab = GetEntity(authoring.bulletPrefab),
            shootingTarget = authoring.shootingTarget,

            shootingRate= authoring.shootingRate,
        });

        AddComponent(new enemyShootTimer
        {
            value = authoring.shootingTimer,
        });

    }
}
