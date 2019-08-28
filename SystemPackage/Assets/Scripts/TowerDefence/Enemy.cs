using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private UIManager _uiManager;
    private Player _player;
    public Animator _animator;
    public System.Action _Killed;
    private WayPointManager.Path _path;
    private NavMeshAgent _agent;
    private int _currentWaypoint = 0;
    private DataLoader _dataLoader;
    private JsonDataSource _enemyData;
    public GameObject _playerObj;
    public bool isDead = false;

    public string _dataSource = "Monster";
    public string _name;
    public float _speed;
    public float _health;
    public int _money;
    public int _defence;

    void Update()
    {
        if (_uiManager._currentHP <= 0.0f)
            Destroy(gameObject);
        if(_health <= 0.0f)
        {
            _currentWaypoint = 0;
            isDead = true;
            _agent.isStopped = true;
            return;
        }
        else
        {
            if(!isDead)
            {
                UpdateAnimation();
                Transform desination = _path.Waypoints[_currentWaypoint];
                _agent.SetDestination(desination.position);
                if (Vector3.Distance(transform.position, desination.position) < 3.0f)
                {
                    if (_currentWaypoint == 3)
                    {
                        _uiManager.UpdatePlayerHP(_uiManager._currentHP - 10.0f);
                        _currentWaypoint = 0;
                        ServiceLocator.Get<GameManager>().UpdatePlayerHP();
                        ServiceLocator.Get<GameManager>().UpdateRequireToWin(1);
                        _Killed?.Invoke();
                    }
                    else
                    {
                        _currentWaypoint++;
                    }
                }
            }
        }
    }

    public void Initialize(WayPointManager.Path path, System.Action Onkilled)
    {
        _path = path;
        _agent = GetComponent<NavMeshAgent>();
        _animator.SetBool("isWalking", true);
        _animator.SetBool("isRunning", true);
        _Killed += Onkilled;
        isDead = false;
        _player = ServiceLocator.Get<Player>();
        _uiManager = ServiceLocator.Get<UIManager>();
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _enemyData = _dataLoader.GetDataSourceById(_dataSource) as JsonDataSource;

        _name = System.Convert.ToString(_enemyData.DataDictionary["Name"]);
        _speed = System.Convert.ToSingle(_enemyData.DataDictionary["Speed"]);
        _health = System.Convert.ToSingle(_enemyData.DataDictionary["Health"]);
        _money = System.Convert.ToInt32(_enemyData.DataDictionary["Money"]);
        _defence = System.Convert.ToInt32(_enemyData.DataDictionary["Defence"]);
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("BasicMovement", _agent.velocity.magnitude + _speed);
    }

    public void TakeDamage(float dmg)
    {
        if(!isDead)
        {
            _health = _health - (dmg - _defence);
            if (_health <= 0)
            {
                isDead = true;
                if(_player == null)
                {
                    _playerObj = GameObject.FindGameObjectWithTag("Player");
                    _player = _playerObj.GetComponent<Player>();
                }
                _player._money += _money;
                ServiceLocator.Get<GameManager>().UpdateRequireToWin(1);
                ServiceLocator.Get<UIManager>().UpdateMoney(_player._money);
                ServiceLocator.Get<UIManager>().UpdatePlayerScore(5);
                this._currentWaypoint = 0;
                StartCoroutine("DeathAnimation");
            }
        }
    }

    public IEnumerator DeathAnimation()
    {

        _animator.SetBool("isWalking", false);
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isDead", true);
        _animator.Play("Death");

        yield return new WaitForSeconds(3.0f);
        _Killed?.Invoke();
        yield return null;
    }
}
