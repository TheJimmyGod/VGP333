using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator _animator;
    private bool _isWalking = false;

    private WayPointManager.Path _path;
    private NavMeshAgent _agent;
    private int _currentWaypoint = 0;
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

    public void Initialize(WayPointManager.Path path)
    {
        _path = path;
        _agent = GetComponent<NavMeshAgent>();
        _animator.SetBool("isWalking", true);
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("movementSpeed", _agent.velocity.magnitude);
    }
}
