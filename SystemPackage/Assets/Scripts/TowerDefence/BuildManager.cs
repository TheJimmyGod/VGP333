using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Player _player;
    private GameObject _gameObject;
    public static BuildManager instance;
    public GameObject _turretPrefab;
    public GameObject _IceturretPrefab;
    public GameObject _FireturretPrefab;
    private GameObject _turret;

    void Awake()
    {
        _player = ServiceLocator.Get<Player>();
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Update()
    {
        if(_player == null)
        {
            _gameObject = GameObject.FindGameObjectWithTag("Player");
            _player = _gameObject.GetComponent<Player>();
        }
        if(_player != null)
        {
            if (_player._Type == Player.Type.Basic)
            {
                _turret = _turretPrefab;
            }
            else if (_player._Type == Player.Type.Fire)
            {
                _turret = _FireturretPrefab;
            }
            else if (_player._Type == Player.Type.Ice)
            {
                _turret = _IceturretPrefab;
            }
        }
    }

    public GameObject GetTurretToBuild()
    {
        return _turret;
    }
}
