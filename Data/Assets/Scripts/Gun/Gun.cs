using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 400.0f;
    
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(BulletSpawnPoint.up * bulletSpeed, ForceMode.Force);
    }
}