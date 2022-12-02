using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ForwardPlayerInput_Component : IComponentData
{
    public bool up;
    public bool right;
    public bool left;

    public bool space;
}
