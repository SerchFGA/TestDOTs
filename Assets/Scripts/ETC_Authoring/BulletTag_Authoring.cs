using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletTag_Authoring : MonoBehaviour
{
    public Vector3 bulletDir;
}

public class BulletTagBaker : Baker<BulletTag_Authoring>
{

    public override void Bake(BulletTag_Authoring authoring)
    {
        AddComponent(new BulletTag());
        AddComponent(new Bullet_Component
        {
            bulletDir = authoring.bulletDir
        });
    }


}
