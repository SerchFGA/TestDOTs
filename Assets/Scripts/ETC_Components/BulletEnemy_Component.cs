using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BulletEnemy_Component : IComponentData
{
    public float3 dir;
}
