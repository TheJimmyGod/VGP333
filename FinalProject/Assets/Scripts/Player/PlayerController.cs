using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
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
    public float _jumpForce = 20.0f;
    public int _jumpCount = 0;
    public bool _isJump = false;
    private bool _isAttack;
    public bool _isdead = false;

    public Animator _animator;
    private GameManager _gameManager;
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;
    private void Awake()
    {
        _controller = GetComponent<Rigidbody2D>();
        ServiceLocator.Register<PlayerController>(this);
        _gameManager = ServiceLocator.Get<GameManager>();
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _playerData = _dataLoader.GetDataSourceById(DataSourceId) as JsonDataSource;

        _name = System.Convert.ToString(_playerData.DataDictionary["Name"]);
        _maxHP = System.Convert.ToSingle(_playerData.DataDictionary["MaxHP"]);
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
        if(!_isAttack)
        {
            if (Input.GetKeyDown(KeyCode.P) && isGround)
            {
                _isAttack = true;
                StartCoroutine("PlayAttackAnimation");
            }

            if (Input.GetKeyDown(KeyCode.W) && _jumpCount <= 2)
            {
                _isJump = true;
                _animator.SetBool("IsJump", true);
                _controller.velocity = Vector2.up * _jumpForce;
                _jumpCount++;
            }
        }
    }

    private IEnumerator PlayAttackAnimation()
    {
        velocity.x = 0.0f;
        _animator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(1.2f);
        _animator.SetBool("IsAttack", false);
        _isAttack = false;
        yield return null;
    }

    void OnCollisionStay2D()
    {
        isGround = true;
        _animator.SetBool("IsJump", false);
        _jumpCount = 0;
    }

    void OnCollisionExit2D()
    {
        isGround = false;
    }

    void FixedUpdate()
    {
        _isJump = false;
    }

    public void TakeDamage(float dmg)
    {
        throw new System.NotImplementedException();
    }
}
