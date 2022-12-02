using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SmallMetTag_Authoring : MonoBehaviour
{
    
}

public class SmallMetTagBaker : Baker<SmallMetTag_Authoring> {

    public override void Bake(SmallMetTag_Authoring authoring)
    {
        AddComponent(new SmallMetTag());
    }

}
