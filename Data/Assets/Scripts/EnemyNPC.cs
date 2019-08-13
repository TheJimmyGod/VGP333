using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNPC : MonoBehaviour, IDamageable
{
    public float attackRange = 10.0f;
    public Transform target;
    public GameObject player;
    public float _currentHP = 30.0f;
    private UIManager _uiManager;
    private NavMeshAgent _agent;
    private Gun _gun;
    private Vector3 start;
    private Vector3 direction;
    private bool sight;
    private bool IsFind = false;
    private Vector3 minSpeed;

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            _uiManager.UpdatePlayerScore(100);
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        _agent = GetComponent<NavMeshAgent>();
        // No null check needed because of RequireComponent attribute
        _gun = GetComponentInChildren<Gun>();
        if(_gun == null)
        {
            Debug.LogError("Enemy has no gun!");
        }
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if(target == null)
        {
            _agent.isStopped = true;
            return;
        }

        start = transform.position;
        direction = player.transform.position - transform.position;
        direction.Normalize();

        RaycastHit hit;
        Ray ray = new Ray(start, direction);
        sight = Physics.Raycast(ray, out hit);
        IsFind = hit.collider.gameObject.CompareTag("Player");

        if (Vector3.Distance(_agent.transform.position, target.position) < attackRange)
        {
            _agent.isStopped = true;
            _agent.transform.LookAt(target.position);
            _gun.Shoot();
        }
        else
        {
            _agent.isStopped = false;
        }

        if(IsFind)
        {
            _agent.SetDestination(target.position);
        }
        else
        {
            minSpeed = Random.insideUnitSphere * 10.0f;
            minSpeed.y = 0.0f;
            _agent.SetDestination(minSpeed);
        }
    }
}
