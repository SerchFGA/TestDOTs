using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct PlayerShooter_Aspect : IAspect
{
    public readonly Entity entity;

    private readonly TransformAspect _transformAspect;
    private readonly RefRO<PlayerShooter_Component> _playerShooter;
    private readonly RefRW<PlayerShooterTimer> _playerShooterTimer;
    //private readonly RefRW<AudioSource> _audioSource;

    public float PlayerShooterTimer
    {
        get => _playerShooterTimer.ValueRO.Value;
        set => _playerShooterTimer.ValueRW.Value = value;
    }

    public bool TimeToShoot => PlayerShooterTimer <= 0;

    public float shootRate => _playerShooter.ValueRO.spawnRate;

    public Entity bulletPrefab => _playerShooter.ValueRO.bullet;

    public Entity spawnPointEntity => _playerShooter.ValueRO.spawnPoint;

    public float3 spawnBulletPos()
    {
        float3 pos;
        //pos = _transformAspect.LocalPosition;

        return new float3(1, 0, 1 + 1.1f);
    }


}
