using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Speed_Authoring : MonoBehaviour
{
    public float value;

}

public class SpeedBaker : Baker<Speed_Authoring> 
{
    public override void Bake(Speed_Authoring authoring)
    {
        AddComponent(new Speed
        {
            value = authoring.value
        });
    }
}

