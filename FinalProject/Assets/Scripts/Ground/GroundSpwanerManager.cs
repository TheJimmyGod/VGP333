using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpwanerManager : MonoBehaviour
{
    public List<GroundSpwaner> Spawners;
    private void Start()
    {
        foreach (var spawner in Spawners)
        {
            spawner.StartSpawner();
        }
    }
}
