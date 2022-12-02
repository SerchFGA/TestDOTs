using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace SFGA.Test
{
    public readonly partial struct Bullet_Aspect : IAspect
    {
        public readonly Entity entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<BulletTag> _bulletTag;
        private readonly RefRW<Bullet_Component> _bulletComponent;

        public float3 bulletDirection
        {
            get => _bulletComponent.ValueRO.bulletDir;
            set => _bulletComponent.ValueRW.bulletDir = value;
        }

        public void ShootBullet(float deltaTime)
        {

            _transformAspect.LocalPosition += new float3(bulletDirection * deltaTime * 20);

        }
    }
}

