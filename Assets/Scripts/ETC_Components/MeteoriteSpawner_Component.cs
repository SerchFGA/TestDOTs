using Unity.Entities;
using Unity.Mathematics;


public struct MeteoriteSpawner_Component : IComponentData
{
    public Entity metoritePrefab;
    public float meteoriteSpawnRate;

    public float xLimit;
    public float zLimit;


    public Entity m_meteoritePrefab;
    public Entity s_meteoritePrefab;
}

public struct secondSpawner_Component : IComponentData
{
    
    public bool isSpawning2ndTime;
    public float3 posSpawning2bdAgain;
}

public struct thirdSpawner_Component : IComponentData
{
    public bool isSpawning3thTime;
    public float3 posSpawning3thAgain;
}

public struct MeteoriteSpawnTimer : IComponentData
{
    public float Value;
}