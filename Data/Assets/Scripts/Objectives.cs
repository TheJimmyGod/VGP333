using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour, IDamageable
{
    public float _currentHP = 1;
    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            ServiceLocator.Get<UIManager>().UpdatePlayerScore(50);
            ServiceLocator.Get<Player>().mCount++;
            ServiceLocator.Get<UIManager>().UpdatePlayerObject(ServiceLocator.Get<Player>().mCount);
            Destroy(this.gameObject);
            if (ServiceLocator.Get<Player>().mCount == 3)
            {
                ServiceLocator.Get<Player>().mCount = 0;
                ServiceLocator.Get<UIManager>().UpdatePlayerObject(0);
                ServiceLocator.Get<GameManager>().requiredTowin = 3;
                ServiceLocator.Get<LevelManager>().SavePlayerData();
                Debug.Log("Excellent!");
            }
            else
            {
                Debug.Log("Get");
            }
        }
    }
}
