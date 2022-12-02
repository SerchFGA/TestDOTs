using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerShooter_Authoring : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawnPoint;


    public float spawnRate;

    public bool isMoving;
}

public class MPlayerShooterBaker : Baker<PlayerShooter_Authoring>
{
    public override void Bake(PlayerShooter_Authoring authoring)
    {
        AddComponent(new PlayerShooter_Component
        {
            bullet = GetEntity(authoring.bullet),
            spawnPoint= GetEntity(authoring.spawnPoint),
            spawnRate = authoring.spawnRate,
            //spawnPointTransform = authoring.spawnPointTransform.position

        });

        AddComponent<PlayerShooterTimer>();

        AddComponent(new isMovingPlayer_Component
        {
            isMoving = authoring.isMoving
        });
    }
}
