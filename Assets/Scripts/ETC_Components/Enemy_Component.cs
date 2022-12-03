using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Enemy_Component : IComponentData
{
    public Entity bulletPrefab;
    public float3 shootingTarget;

    public float shootingRate;
}

public struct enemyShootTimer : IComponentData
{
    public float value;
}

