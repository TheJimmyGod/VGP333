using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 10.0f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Was Shot");   
        }
        var damageable = collision.gameObject.GetComponentInChildren<IDamageable>();
        if(damageable!=null)
        {
            damageable.TakeDamage(bulletDamage);
        }
    }
}
