﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public string DataSourceId = "FlyingEnemyData";
    public string _name;
    public float _maxHP;
    public float _currHP;
    public float _speed;
    public float _damage;
    public float _exp;
    public float _startDazedTime;
    public AudioSource _audioSource;
    private float _dazedTime;

    private float _timeAttack;
    public float _startTimeAttack = 4.0f;

    public Transform BulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 2.0f;

    private GameManager _gameManager;
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;

    public Vector2 velocity;
    public Vector2 pos;

    public GameObject _hero;
    public Rigidbody2D rb;
    public System.Action _Killed;
    public void Initialize(System.Action Onkilled)
    {
        _Killed += Onkilled;
        rb = GetComponent<Rigidbody2D>();

        _gameManager = ServiceLocator.Get<GameManager>();
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _playerData = _dataLoader.GetDataSourceById(DataSourceId) as JsonDataSource;
        _audioSource = GetComponent<AudioSource>();
        _name = System.Convert.ToString(_playerData.DataDictionary["Name"]);
        _maxHP = System.Convert.ToSingle(_playerData.DataDictionary["MaxHP"]);
        _speed = System.Convert.ToSingle(_playerData.DataDictionary["Speed"]);
        _damage = System.Convert.ToSingle(_playerData.DataDictionary["Damage"]);
        _exp = System.Convert.ToSingle(_playerData.DataDictionary["Exp"]);
        if (ServiceLocator.Get<Enemy>() == null)
        {
            ServiceLocator.Register<Enemy>(this);
        }
        _currHP = _maxHP;
        velocity.x = 0.5f;
        velocity.y = transform.position.y;
        _hero = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (_hero == null)
        {
            _hero = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {

            if (_dazedTime <= 0)
            {
                velocity.x = 0.5f;
                velocity.y = transform.position.y;
            }
            else
            {
                velocity.x = 0.0f;
                _dazedTime -= Time.deltaTime;
            }
            transform.Translate((-velocity * Time.deltaTime) * _speed);
            if (_timeAttack <= 0)
            {
                if ((_hero.gameObject.transform.position.y - transform.position.y) >= -1)
                {
                    _audioSource.Play();
                    ShootBullet();
                }
                _timeAttack = _startTimeAttack;
            }
            else
            {
                _timeAttack -= Time.deltaTime;
            }
            if (this.transform.position.x < -12.0f)
            {
                _Killed?.Invoke();
            }
        }
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * bulletSpeed;
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("Ouch!");
        _dazedTime = _startDazedTime;
        _currHP -= dmg;
        if (_currHP <= 0)
        {
            ServiceLocator.Get<PlayerController>()._exp += _exp;
            _gameManager.UpdateScore(100);
            _gameManager.UpdateEXP(_exp);
            _gameManager._requiredToWin++;
            _Killed?.Invoke();
        }
    }
}
