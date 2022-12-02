using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BorderTag_Authoring : MonoBehaviour
{

}
public class BorderTagBaker : Baker<BorderTag_Authoring>
{
    public override void Bake(BorderTag_Authoring authoring)
    {
        AddComponent(new BorderTag());
    }
}
