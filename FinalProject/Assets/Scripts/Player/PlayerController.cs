using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string DataSourceId = "YeonData";
    public bool isGround;
    public Vector2 velocity;
    public Vector2 pos;
    public Rigidbody2D _controller;

    public string _name;
    public float _maxHP;
    public float _currHP;
    public float _speed;
    public float _damage;
    public float _exp;
    public float _jumpForce = 3.0f;

    private bool _isJump;
    private bool _isAttack;

    public Animator _animator;
    private GameManager _gameManager;
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;
    private void Awake()
    {
        _controller = GetComponent<Rigidbody2D>();
        ServiceLocator.Register<PlayerController>(this);
        _gameManager = ServiceLocator.Get<GameManager>();

        _playerData = _dataLoader.GetDataSourceById(DataSourceId) as JsonDataSource;

        _name = System.Convert.ToString(_playerData.DataDictionary["Name"]);
        _currHP = System.Convert.ToSingle(_playerData.DataDictionary["Health"]);
        _speed = System.Convert.ToSingle(_playerData.DataDictionary["Speed"]);
        _damage = System.Convert.ToSingle(_playerData.DataDictionary["Damage"]);
        _exp = System.Convert.ToSingle(_playerData.DataDictionary["Exp"]);
        _currHP = _maxHP;
    }

    void Update()
    {
        pos = this.transform.position;
        float mHorizontal = Input.GetAxisRaw("Horizontal") * _speed;
        _animator.SetFloat("Speed", Mathf.Abs(mHorizontal));
        Vector2 move = new Vector2(mHorizontal, 0.0f);
        _controller.AddForce(move);
        if(Input.GetKeyDown(KeyCode.P))
        {
            _isAttack = true;
            _animator.SetBool("HeroAttack", true);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
            _animator.SetBool("IsJump", true);
        }
    }

    void OnCollisionStay2D()
    {
        isGround = true;
        _animator.SetBool("IsJump", false);
    }

    void OnCollisionExit2D()
    {
        isGround = false;
    }

    void FixedUpdate()
    {
        _isJump = false;
    }
}
