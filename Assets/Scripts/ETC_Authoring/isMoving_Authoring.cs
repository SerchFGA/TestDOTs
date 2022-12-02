using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class isMoving_Authoring : MonoBehaviour
{
    public bool value;
}

public class isMovingBaker : Baker<isMoving_Authoring>
{
    public override void Bake(isMoving_Authoring authoring)
    {
        AddComponent(new isMoving
        {
            value = authoring.value
        });
    }
}
