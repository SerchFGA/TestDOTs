using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using TMPro;

public class GM_Authoring : MonoBehaviour
{
    public TMP_Text pointsText;
    public float points
    {
        get
        {
            return pts;
        }
        set
        {
            pts = value;
            pointsText.text = pts+" pts.";
        }
    }
    private float pts;

    public TMP_Text LivesText;
    public int lives
    {
        get
        {
            return liv;
        }
        set
        {
            liv = value;
            LivesText.text = liv + " Lives";
        }
    }
    private int liv;

    private void Start()
    {
        
    }
}

public class GM_Baker : Baker<GM_Authoring>
{

    public override void Bake(GM_Authoring authoring)
    {
        AddComponent(new GM_Component
        {
            points = authoring.points,
            lives= authoring.lives
        });

        AddComponent(new GM_Tag());
    }


}
