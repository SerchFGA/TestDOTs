using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PlayerShooter_Component : IComponentData
{
    public Entity bullet;
    public Entity spawnPoint;
 
    public float spawnRate;
}

public struct PlayerShooterTimer : IComponentData
{
    public float Value;
}

public struct isMovingPlayer_Component : IComponentData
{
    public bool isMoving;
}

