using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject _turretPrefab;
    private GameObject _turret;

    void Start()
    {
        _turret = _turretPrefab;
    }

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return _turret;
    }
}
