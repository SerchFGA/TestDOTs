using Unity.Entities;
using Unity.Transforms;

namespace SFGA.Test
{
    public readonly partial struct PlayerShooter_Aspect : IAspect
    {
        public readonly Entity entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<PlayerShooter_Component> _playerShooter;
        private readonly RefRW<PlayerShooterTimer> _playerShooterTimer;
        private readonly RefRW<isMovingPlayer_Component> _isMovingPlayer;


        public float PlayerShooterTimer
        {
            get => _playerShooterTimer.ValueRO.Value;
            set => _playerShooterTimer.ValueRW.Value = value;
        }

        public bool TimeToShoot => PlayerShooterTimer <= 0;

        public float shootRate => _playerShooter.ValueRO.spawnRate;

        public Entity bulletPrefab => _playerShooter.ValueRO.bullet;

        public Entity spawnPointEntity => _playerShooter.ValueRO.spawnPoint;


        public bool isMovingPlayer
        {
            get => _isMovingPlayer.ValueRO.isMoving;
            set => _isMovingPlayer.ValueRW.isMoving = value;
        }

    }
}

