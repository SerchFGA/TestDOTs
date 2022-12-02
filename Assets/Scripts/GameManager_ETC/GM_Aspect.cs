using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using TMPro;

public readonly partial struct GM_Aspect : IAspect
{
    public readonly Entity entity;

    private readonly TransformAspect _transformAspect;
    private readonly RefRO<GM_Tag> _GMTag;
    private readonly RefRW<GM_Component> _GMComponent;

    public int Lives
    {
        get => _GMComponent.ValueRO.lives;
        set => _GMComponent.ValueRW.lives= value;
    }

    public float Points
    {
        get => _GMComponent.ValueRO.points;
        set => _GMComponent.ValueRW.points = value;
    }

    
    public void setLives()
    {
        GameManager_Script.Instance.SpaceShipDestroy();
    }
}
