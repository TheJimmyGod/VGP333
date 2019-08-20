using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Tower : MonoBehaviour
{
    public Transform target;
    public Transform partToRotate;
    private float shotTime;
    private float autoTime;

    private UIManager _uiManager;
    private NavMeshAgent _agent;
    private DataLoader _dataLoader;
    private JsonDataSource _towerData;
    private Gun _gun;

    public string DataSourceId = "BasicTower";
    public string _name;
    public float _range;
    public float _damage;
    public float _speed;
    public float _attackRate;
    public int _cost;

    private void Awake()
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        _agent = GetComponent<NavMeshAgent>();
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _towerData = _dataLoader.GetDataSourceById(DataSourceId) as JsonDataSource;

        _damage = System.Convert.ToSingle(_towerData.DataDictionary["Damage"]);
        _range = System.Convert.ToSingle(_towerData.DataDictionary["Range"]);
        _speed = System.Convert.ToSingle(_towerData.DataDictionary["Speed"]);
        _attackRate = System.Convert.ToSingle(_towerData.DataDictionary["AttackRate"]);
        _cost = System.Convert.ToInt32(_towerData.DataDictionary["Cost"]);
        _name = System.Convert.ToString(_towerData.DataDictionary["Name"]);

        _gun = GetComponentInChildren<Gun>();
        if (_gun == null)
        {
            Debug.LogError("Tower has no weapon!");
        }
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float _shortestDistance = Mathf.Infinity;
        GameObject _nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < _shortestDistance)
            {
                _shortestDistance = distanceToEnemy;
                _nearestEnemy = enemy;
            }
        }

        if (_nearestEnemy != null && _shortestDistance <= _range)
        {
            target = _nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }



    void Update()
    {
        shotTime += Time.deltaTime;

        if (target == null)
        {
            _agent.isStopped = true;
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion _lookRot = Quaternion.LookRotation(dir);
        Vector3 rot = _lookRot.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rot.y,0f);



        if (Vector3.Distance(_agent.transform.position, target.position) < _range)
        {
            _agent.isStopped = true;
            _agent.transform.LookAt(target.position);
            if(shotTime > (autoTime * _attackRate))
            {
                autoTime++;
                _gun.Shoot();
            }

        }
        else
        {
            _agent.isStopped = false;
        }
    }
}
