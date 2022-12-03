using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BulletEnemyTag_Authoring : MonoBehaviour
{
    public float3 dir;
}

public class BulletEnemyTagBaker : Baker<BulletEnemyTag_Authoring>
{

    public override void Bake(BulletEnemyTag_Authoring authoring)
    {
        AddComponent(new BulletEnemyTag());
        AddComponent(new BulletEnemy_Component
        {
            dir = authoring.dir,
        });
    }


}
