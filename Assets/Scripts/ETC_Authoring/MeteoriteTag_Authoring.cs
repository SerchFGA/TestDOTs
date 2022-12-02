using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MeteoriteTag_Authoring : MonoBehaviour
{
    
}

public class MeteoriteTagBaker : Baker<MeteoriteTag_Authoring> {

    public override void Bake(MeteoriteTag_Authoring authoring)
    {
        AddComponent(new MeteoriteTag());
    }

}
