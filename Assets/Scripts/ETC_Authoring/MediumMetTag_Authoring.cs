using UnityEngine;
using Unity.Entities;

public class MediumMetTag_Authoring : MonoBehaviour
{
    
}

public class MediumMetTagBaker : Baker<MediumMetTag_Authoring> {

    public override void Bake(MediumMetTag_Authoring authoring)
    {
        AddComponent(new MediumMetTag());
    }

}
