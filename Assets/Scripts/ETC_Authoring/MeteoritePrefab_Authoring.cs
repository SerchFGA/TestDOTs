using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MeteoritePrefab_Authoring : MonoBehaviour
{
    public int number;
    //public GameObject prefab;
}

public class MeteoriteBaker : Baker<MeteoritePrefab_Authoring>
{
    public override void Bake(MeteoritePrefab_Authoring authoring)
    {
        AddComponent(new MeteoritePrefab
        {
            number = authoring.number,
            //prefab = authoring.prefab
        });
    }
}
