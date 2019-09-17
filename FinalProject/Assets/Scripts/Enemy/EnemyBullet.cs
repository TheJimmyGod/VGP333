﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletDmg;
    public GameObject _GO;
    private PlayerController _hero;
    void Awake()
    {
        bulletDmg = ServiceLocator.Get<Enemy>()._damage;
        _hero = ServiceLocator.Get<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _hero.TakeDamage(bulletDmg);
            Destroy(gameObject);
        }
    }
}
