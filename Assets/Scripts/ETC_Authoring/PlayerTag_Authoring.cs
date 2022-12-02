using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerTag_Authoring : MonoBehaviour
{
 
}

public class PlayerTagBaker : Baker<PlayerTag_Authoring>
{

    public override void Bake(PlayerTag_Authoring authoring)
    {
        AddComponent(new PlayerTag());
    }

}
