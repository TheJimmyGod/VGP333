using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 0.0f;

    void Awake()
    {
        bulletDamage = ServiceLocator.Get<Tower>()._damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponentInChildren<IDamageable>();
        if(damageable!=null)
        {
            damageable.TakeDamage(bulletDamage);
        }
    }
}
