using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public List<Spwaner> Spawners;
    public List<FlyingSpwaner> FSpawners;
    private void Awake()
    {
        foreach (var spawner in Spawners)
        {
            spawner.Init();
        }
        foreach (var spawner in FSpawners)
        {
            spawner.Init();
        }
    }

    private void Start()
    {
        foreach (var spawner in Spawners)
        {
            spawner.StartSpawner();
        }
        foreach (var spawner in FSpawners)
        {
            spawner.StartSpawner();
        }
    }
}
