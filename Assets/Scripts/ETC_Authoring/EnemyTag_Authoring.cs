using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyTag_Authoring : MonoBehaviour
{
  
}

public class EnemyTagBaker : Baker<EnemyTag_Authoring>
{
    public override void Bake(EnemyTag_Authoring authoring)
    {
        AddComponent(new EnemyTag_Component());

    }
}
