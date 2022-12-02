using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using TMPro;

public struct GM_Component : IComponentData
{
    //public TMP_Text pointText;
    public float points;

    //public TMP_Text livesText;
    public int lives;
}

public struct GM_Tag : IComponentData
{
}
