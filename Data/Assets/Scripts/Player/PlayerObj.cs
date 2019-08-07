using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    public string Name;
    public string Class;
    public int MaxHealth;
    public int NumLives;
    public float RunSpeed;

    public string DataSourceId = "PlayerData";
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;
    private void Awake()
    {
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _playerData =_dataLoader.GetDataByName(DataSourceId) as JsonDataSource;

        MaxHealth = System.Convert.ToInt32(_playerData.DataDictionary["MaxHealth"]);
        NumLives = System.Convert.ToInt32(_playerData.DataDictionary["NumLives"]);
        RunSpeed = System.Convert.ToSingle(_playerData.DataDictionary["RunSpeed"]);
        Name = System.Convert.ToString(_playerData.DataDictionary["Name"]);
        Class = System.Convert.ToString(_playerData.DataDictionary["Class"]);
    }
}