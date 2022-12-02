using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ForwardPlayerInput_Authoring : MonoBehaviour
{
    public bool up;
    public bool right;
    public bool left;

    public bool space;
}

public class ForwardPlayerInputBaker : Baker<ForwardPlayerInput_Authoring>
{
    public override void Bake(ForwardPlayerInput_Authoring authoring)
    {
        AddComponent(new ForwardPlayerInput_Component
        {
            up = authoring.up,
            right = authoring.right,
            left = authoring.left,
            space = authoring.space
            
        }); ;
    }
}
