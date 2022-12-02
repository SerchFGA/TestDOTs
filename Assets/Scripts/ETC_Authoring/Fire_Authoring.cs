using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class Fire_Authoring : MonoBehaviour
{
    public float3 position;
    public Rotation rotation;
}

public class FireBaker : Baker<Fire_Authoring>
{
    public override void Bake(Fire_Authoring authoring)
    {
        AddComponent(new Fire_Component
        {
            position= authoring.position,
            rotation= authoring.rotation,
        });

        AddComponent<FireTag>();

    }
}
