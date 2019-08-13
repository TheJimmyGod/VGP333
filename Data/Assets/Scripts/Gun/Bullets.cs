using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float bulletDamage = 0.0f;
    public int GunTypeNumbers = 0;

    private GameObject _guns = null;
    public void Awake()
    {
        _guns = GameObject.FindGameObjectWithTag("PlayerGun");
        if(_guns == null)
        {
            Debug.Log("Not exist");
        }
        if(_guns.GetComponent<Guns>().GunTypeNumber == 0)
            bulletDamage = _guns.GetComponent<Guns>().P_Damage;
        else if (_guns.GetComponent<Guns>().GunTypeNumber == 1)
            bulletDamage = _guns.GetComponent<Guns>().S_Damage;
        else if (_guns.GetComponent<Guns>().GunTypeNumber == 2)
            bulletDamage = _guns.GetComponent<Guns>().M_Damage;

    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null && gameObject.CompareTag("Enemy"))
        {
            damageable.TakeDamage(bulletDamage);
            Destroy(this);
        }
    }
}
