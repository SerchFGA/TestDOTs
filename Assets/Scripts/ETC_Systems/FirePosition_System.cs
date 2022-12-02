using Unity.Entities;
using Unity.Transforms;

namespace SFGA.Test
{
    //System to move fire from player spaceship
    public partial class FirePosition_System : SystemBase
    {
        protected override void OnUpdate()
        {
            //Get Entities
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var fireEntity = SystemAPI.GetSingletonEntity<FireTag>();

            //Get Aspects from Entities
            var playerShooter = SystemAPI.GetAspectRW<PlayerShooter_Aspect>(playerEntity);
            var playerTransform = SystemAPI.GetAspectRW<TransformAspect>(playerEntity);

            EntityCommandBuffer ECB = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            //Check if Player is Moving
            if (playerShooter.isMovingPlayer)
            {
                //Set fire entity pos && rot
                ECB.SetComponent(fireEntity, new Translation
                {
                    Value = playerTransform.Position
                });

                ECB.SetComponent(fireEntity, new Rotation
                {
                    Value = playerTransform.Rotation
                });
            }
            else
            {
                ECB.SetComponent(fireEntity, new Translation
                {
                    Value = new Unity.Mathematics.float3(0, 0, -50)
                });
            }
        }
    }
}

