using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int score;
    public string playerName;
    public float C_Pistol;
    public float T_Pistol;
    public float C_ShotGun;
    public float T_ShotGun;
    public float C_Machine;
    public float T_Machine;
    public void SavePlayerData()
    {
        Core.Debug.Log("Save Player Data");
        ServiceLocator.Get<PlayerData>().SavePlayerData(score, playerName);
        ServiceLocator.Get<GunData>().SaveGunData(C_Pistol,C_ShotGun,C_Machine,
            T_Pistol,T_ShotGun,T_Machine);
    }

    private void Awake()
    {
        ServiceLocator.Register<LevelManager>(this);
        ServiceLocator.Get<UIManager>().gameObject.SetActive(true);
    }
}
