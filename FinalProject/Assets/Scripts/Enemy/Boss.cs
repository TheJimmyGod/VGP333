﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{

    public string DataSourceId = "BossData";
    public string _name;
    public float _maxHP;
    public float _currHP;
    public float _speed;
    public float _damage;
    public float _exp;
    public float _startDazedTime;
    private float _dazedTime;

    private float _timeAttack;
    public float _startTimeAttack = 1.0f;

    public Transform _attackPos;
    public LayerMask _layer_Player;
    public Vector2 _range;

    public Animator _animator;
    private GameManager _gameManager;
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;

    public Vector2 velocity;
    public Vector2 pos;
    public AudioSource _audioSource;
    public GameObject _hero;
    public Rigidbody2D rb;
    public System.Action _Killed;
    void Awake()
    {
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
        _audioSource = GetComponent<AudioSource>();
        if (ServiceLocator.Get<Boss>() == null)
        {
            ServiceLocator.Register<Boss>(this);
        }
        _currHP = _maxHP;
        velocity.x = 1.1f;
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
                velocity.x = 1.1f;
            }
            else
            {
                velocity.x = 0.0f;
                _dazedTime -= Time.deltaTime;
            }
            if(transform.position.x == -5)
            {
                transform.Translate((velocity * Time.deltaTime) * _speed);
            }
            else if (transform.position.x == 5)
            {
                transform.Translate((-velocity * Time.deltaTime) * _speed);
            }
            _animator.SetFloat("Speed", Mathf.Abs(_speed));
            if (_timeAttack <= 0)
            {
                if (Mathf.Abs(_hero.transform.position.x - transform.position.x) < 3f)
                {
                    StartCoroutine("PlayAttack");
                }
                _timeAttack = _startTimeAttack;
            }
            else
            {
                velocity.x = 1.1f;
                _timeAttack -= Time.deltaTime;
            }
            if (this.transform.position.y < -10.0f)
            {
                Destroy(gameObject);
                _gameManager._isBossKilled = true;
            }
        }
    }

    private IEnumerator PlayAttack()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.up * 10.0f;
        _audioSource.Play();
        Collider2D _meleeAttack = Physics2D.OverlapBox(_attackPos.position, new Vector2(_range.x, _range.y), 0, _layer_Player);
        if (_hero && _meleeAttack)
        {
            _meleeAttack.GetComponent<PlayerController>().TakeDamage(_damage);
            _meleeAttack.GetComponent<PlayerController>().transform.Translate(-Vector2.right * 10.0f);
        }
    }


    public void TakeDamage(float dmg)
    {
        Debug.Log("Ouch!");
        _dazedTime = _startDazedTime;
        _currHP -= dmg;
        if (_currHP <= 0)
        {
            ServiceLocator.Get<PlayerController>()._exp += _exp;
            Destroy(gameObject);
            _gameManager._isBossKilled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackPos.position, new Vector3(_range.x, _range.y, 1));
    }
}