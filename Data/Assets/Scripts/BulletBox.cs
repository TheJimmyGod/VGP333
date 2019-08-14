using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour,IDamageable
{
    public float _currentHP = 1;
    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            ServiceLocator.Get<UIManager>().UpdatePlayerScore(50);
            ServiceLocator.Get<Player>().GetAmmosByItem();
            Destroy(this.gameObject);
        }
    }
}
