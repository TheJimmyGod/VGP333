using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnerManager : MonoBehaviour
{
    public List<UnitSpawner> Spawners;
    public WayPointManager WayPointManager;

    private void Awake()
    {
        foreach(var spawner in Spawners)
        {
            spawner.Init(WayPointManager.GetPath(spawner.pathId));
        }
    }

    private void Start()
    {
        foreach (var spawner in Spawners)
        {
            spawner.StartSpawner();
        }
    }
}
