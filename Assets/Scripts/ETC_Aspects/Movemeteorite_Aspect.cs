using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct Movemeteorite_Aspect : IAspect
{
    private readonly Entity entity;


    private readonly TransformAspect transformAspect;
    private readonly RefRW<isMoving> isMoving;
    private readonly RefRO<Speed> speed;
    private readonly RefRW<TargetMovement> movement;

    public void setDirection(RefRW<RandomComponent> randomComponent)
    {
        if (!isMoving.ValueRW.value)
        {
            movement.ValueRW.value = GetRandomDirection(randomComponent);
            isMoving.ValueRW.value = true;
            //Debug.Log("Value: " + movement.ValueRW.value);
        }

    }

    public void Move(float deltaTime)
    {
 
        transformAspect.Position += new float3(movement.ValueRW.value.x * deltaTime * speed.ValueRO.value, 0, movement.ValueRW.value.z * deltaTime * speed.ValueRO.value);

        CheckBorders(transformAspect);
    }


    void CheckBorders(TransformAspect transformAspect)
    {
        float UpBorder = 16;
        float DownBorder = -16;
        float RightBorder = 27;
        float LeftBorder = -27;

        var pos = transformAspect.Position;
        var x = pos.x;
        var z = pos.z;

        if (x > RightBorder)
        {
            pos.x = LeftBorder;
            transformAspect.Position = pos;
        }
        if (x < LeftBorder)
        {
            pos.x = RightBorder;
            transformAspect.Position = pos;
        }
        if (z > UpBorder)
        {
            pos.z = DownBorder;
            transformAspect.Position = pos;
        }
        if (z < DownBorder)
        {
            pos.z = UpBorder;
            transformAspect.Position = pos;
        }
    }

    private float3 GetRandomDirection(RefRW<RandomComponent> randomComponent)
    {
        return new float3(
            randomComponent.ValueRW.random.NextFloat(-1, 1), 
            0, 
            randomComponent.ValueRW.random.NextFloat(-1, 1));
    }

}
