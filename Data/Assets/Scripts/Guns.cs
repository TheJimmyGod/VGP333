using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//{
//  "Damage": 20,
//  "Range": 100,
//  "Speed": 8,
//  "Ammo": 8,
//  "MaxAmmo": 40,
//  "Name": "Pistol"
//}

public class Guns : MonoBehaviour
{
    public string Name;
    public float Range;
    public int Damage;
    public int MaxAmmo;
    public int Ammo;
    public float Speed;

    public string pistolId = "PistolData";
    public string shotGunId = "ShotGunData";
    public string machineGunId = "MachineGunData";
    private DataLoader _dataLoader;
    private JsonDataSource _gunData;
    private void Awake()
    {
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _gunData = _dataLoader.GetDataByName(pistolId) as JsonDataSource;
    }

    void Update()
    {
        
    }
}
