using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Tower _towerSet;
    public float bulletDamage = 0.0f;

    void Awake()
    {
        _towerSet = ServiceLocator.Get<Tower>();
        bulletDamage = _towerSet._damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable!=null)
        {
            damageable.TakeDamage(bulletDamage);
        }
    }
}
