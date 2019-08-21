using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject bulletPrefab;
    private Tower _towerSet;
    public float bulletSpeed = 0.0f;
    
    void Awake()
    {
        _towerSet = ServiceLocator.Get<Tower>();
        bulletSpeed = _towerSet._speed;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(BulletSpawnPoint.up * bulletSpeed, ForceMode.Force);
    }
}