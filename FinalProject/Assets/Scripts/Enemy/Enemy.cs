using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public string DataSourceId = "EnemyData";
    public string _name;
    public float _maxHP;
    public float _currHP;
    public float _speed;
    public float _damage;
    public float _exp;

    public Animator _animator;
    private GameManager _gameManager;
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;

    public Vector2 velocity;
    public Vector2 pos;

    public PlayerController _hero;
    public Rigidbody2D rb;
    public System.Action _Killed;
    public void Initialize(System.Action Onkilled)
    {
        _Killed += Onkilled;
        rb = GetComponent<Rigidbody2D>();
        _gameManager = ServiceLocator.Get<GameManager>();
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _playerData = _dataLoader.GetDataSourceById(DataSourceId) as JsonDataSource;

        _name = System.Convert.ToString(_playerData.DataDictionary["Name"]);
        _maxHP = System.Convert.ToSingle(_playerData.DataDictionary["MaxHP"]);
        _speed = System.Convert.ToSingle(_playerData.DataDictionary["Speed"]);
        _damage = System.Convert.ToSingle(_playerData.DataDictionary["Damage"]);
        _exp = System.Convert.ToSingle(_playerData.DataDictionary["Exp"]);
        _animator = GetComponent<Animator>();
        _currHP = _maxHP;
        velocity.x = 0.5f;
        _hero = ServiceLocator.Get<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_hero == null)
        {
            _hero = ServiceLocator.Get<PlayerController>();
        }
        if (!_hero._isdead)
        {
            transform.Translate((-velocity * Time.deltaTime) * _speed);
            _animator.SetFloat("Speed", Mathf.Abs(_speed));
            if ((_hero.gameObject.transform.position.y - transform.position.y) >= -1)
            {
                ShootBullet();
            }

            if ((_hero._isJump))
            {
                rb.velocity = Vector2.up * 1.5f;
            }
        }
    }

    public void ShootBullet()
    {

    }

    public void TakeDamage(float dmg)
    {
        _currHP -= dmg;
        if(_currHP <= 0)
        {
            _hero._exp += _exp;
        }
    }
}
