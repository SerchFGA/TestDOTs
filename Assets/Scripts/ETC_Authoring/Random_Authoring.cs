using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Random_Authoring : MonoBehaviour
{
    
}

public class RandomBaker : Baker<Random_Authoring>
{
    public override void Bake(Random_Authoring authoring)
    {
        AddComponent(new RandomComponent
        {
            random = new Unity.Mathematics.Random(85)
        });
    }
}
