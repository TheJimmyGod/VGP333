using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int score;
    public string playerName;
    public void SavePlayerData()
    {
        Core.Debug.Log("Save Player Data");
        ServiceLocator.Get<PlayerData>().SavePlayerData(score, playerName);
    }

    private void Awake()
    {
        ServiceLocator.Get<UIManager>().gameObject.SetActive(true);
    }
}
