using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public List<Spwaner> Spawners;

    private void Awake()
    {
        foreach (var spawner in Spawners)
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
    }
}
