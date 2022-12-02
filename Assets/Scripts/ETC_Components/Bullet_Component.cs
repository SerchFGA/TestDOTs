using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Bullet_Component : IComponentData
{
    public float3 bulletDir;
}
