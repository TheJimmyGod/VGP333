using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int score;
    public int money;
    public int wave;
    public void SavePlayerData()
    {
        ServiceLocator.Get<PlayerData>().SavePlayerData(score,wave);
    }

    private void Awake()
    {
        ServiceLocator.Register<LevelManager>(this);
        ServiceLocator.Get<UIManager>().gameObject.SetActive(true);
    }
}
