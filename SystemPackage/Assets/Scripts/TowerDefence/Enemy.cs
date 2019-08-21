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

    public string _dataSource = "Monster";
    public string _name;
    public float _speed;
    public float _health;
    public int _money;
    public int _defence;
    void Awake()
    {
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

    void Update()
    {
        UpdateAnimation();
        Transform desination = _path.Waypoints[_currentWaypoint];
        _agent.SetDestination(desination.position);
        if(Vector3.Distance(transform.position, desination.position) < 3.0f)
        {
            if(_currentWaypoint == 3)
            {
                _uiManager.UpdatePlayerHP(_uiManager._currentHP - 10.0f);
                Destroy(gameObject);
                _Killed();
            }
            else
            {
                _currentWaypoint++;
            }
        }

        if(_health <= 0.0f)
        {

            _Killed?.Invoke();
            return;
        }
    }

    public void Initialize(WayPointManager.Path path, System.Action Onkilled)
    {
        _path = path;
        _agent = GetComponent<NavMeshAgent>();
        _animator.SetBool("isWalking", true);
        _Killed += Onkilled;
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("movementSpeed", _agent.velocity.magnitude);
    }

    public void TakeDamage(float dmg)
    {
        _health = _health - (dmg - _defence);
        if (_health <= 0)
        {
            _player._money += _money;
            ServiceLocator.Get<UIManager>().UpdateMoney(_money);
            Destroy(gameObject);
            _Killed();
        }
    }

    void OnDestroy()
    {
        _Killed = null;
        Debug.Log("으악");
    }
}
