using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SFGA.Test
{
    public readonly partial struct Enemy_Aspect : IAspect
    {
        private readonly Entity entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<Speed> speed;
        private readonly RefRW<TargetMovement> movement;

        //Move Enemies
        public void Move(float deltaTime)
        {
            _transformAspect.Position += new float3(movement.ValueRW.value.x * deltaTime * speed.ValueRO.value, 0, movement.ValueRW.value.z * deltaTime * speed.ValueRO.value);

            CheckBorders(_transformAspect);
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
    }
}

