using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class TargetMovement_Authoring : MonoBehaviour
{
    public float3 value;

}

public class Targetmovement : Baker<TargetMovement_Authoring>
{
    
    public override void Bake(TargetMovement_Authoring authoring)
    {
       

        AddComponent(new TargetMovement
        {

            value = authoring.value
        });
    }


}
