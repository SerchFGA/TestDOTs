using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Fire_Component : IComponentData
{
    public float3 position;
    public Rotation rotation;
}

public struct FireTag : IComponentData
{

}

