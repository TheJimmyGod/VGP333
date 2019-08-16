using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public Animator _animator;
    public System.Action _Killed;
    private bool _isWalking = false;
    private WayPointManager.Path _path;
    private NavMeshAgent _agent;
    private int _currentWaypoint = 0;
    private float _currentHealth = 10.0f;

    void Update()
    {
        UpdateAnimation();
        Transform desination = _path.Waypoints[_currentWaypoint];
        _agent.SetDestination(desination.position);
        if(Vector3.Distance(transform.position, desination.position) < 3.0f)
        {
            _currentWaypoint++;
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
        _currentHealth -= dmg;
        if(_currentHealth <= 0)
        {
            _Killed();
        }
    }
}
