using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace SFGA.Test
{
    [BurstCompile]
    public partial struct EnemyMoving_iSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state){}
        [BurstCompile]
        public void OnDestroy(ref SystemState state){}
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Call job to move Enemy
            float deltaTime = SystemAPI.Time.DeltaTime;
            new MoveEnemy
            {
                deltaTime= deltaTime,
            }.ScheduleParallel();

        }

        // Job to Move Enemy
        [BurstCompile]
        public partial struct MoveEnemy : IJobEntity
        {
            public float deltaTime;
            public void Execute(Enemy_Aspect enemy )
            {
                enemy.Move(deltaTime);
            }
        }
    }
}

