using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics.Aspects;
using Unity.Collections;

namespace SFGA.Test
{
    [UpdateAfter(typeof(PlayerInput_System))]
    [BurstCompile]
    public partial struct PlayerMoving_iSystem : ISystem
    {
        //Get Inputs from System
        private SystemHandle _playerInputHandle;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _playerInputHandle = state.World.GetExistingSystem<PlayerInput_System>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var shouldMove = SystemAPI.GetComponent<ForwardPlayerInput_Component>(_playerInputHandle);

            //Get player Entity 
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetAspectRW<TransformAspect>(playerEntity);
            var rb = SystemAPI.GetAspectRW<RigidBodyAspect>(playerEntity);

            var playerShooter = SystemAPI.GetAspectRW<PlayerShooter_Aspect>(playerEntity);

            float rotationSpeed = 0.03f;
            //Move forward
            if (shouldMove.up)
            {
                rb.ApplyLinearImpulseLocalSpace(new float3(0f, 0f, 1));
                playerShooter.isMovingPlayer = true;
            }
            else
            {
                playerShooter.isMovingPlayer = false;
            }

            //Rotate Right
            if (shouldMove.right)
            {
                rb.ApplyAngularImpulseLocalSpace(new float3(0f, rotationSpeed, 0f));
            }

            //Rotate Left
            if (shouldMove.left)
            {
                rb.ApplyAngularImpulseLocalSpace(new float3(0f, -rotationSpeed, 0f));
            }

            CheckBorders(playerTransform);

        }

        //Check Border to infinite move meteorites
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

