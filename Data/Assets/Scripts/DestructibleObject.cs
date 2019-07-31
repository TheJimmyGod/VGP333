using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructibleObjects : MonoBehaviour, IDamageable
{
    public float MaxHealth = 100.0f;
    public float _currentHealth;
    private bool _isDying = false;
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0.0f)
        {
            _isDying = true;
            DestroyMe();
        }
    }
    private void Awake()
    {
        _currentHealth = MaxHealth;
    }

    void Update()
    {
        if (_isDying)
        {
            return;
        }
    }

    private void DestroyMe()
    {
        if (_currentHealth <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
