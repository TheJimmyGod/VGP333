using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private UIManager _uiManager;
    private GameObject turret;
    public Color _hoverColor;
    private Color _startColor;
    private Renderer _render;
    public GameObject _gameObject;
    public Player _player;
    public Vector3 _pos;
    public Tower _basicTower;

    void Awake()
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        _gameObject = GameObject.FindGameObjectWithTag("Player");
        _player = _gameObject.GetComponent<Player>();
        if (_player == null)
            Debug.Log("Player doesn't exist!");
        _render = GetComponent<Renderer>();
        _startColor = _render.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }
        if (_player._money >= 5)
        {
            if (_player._Type == Player.Type.Basic)
            {
                GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
                turret = (GameObject)Instantiate(turretToBuild, transform.position + _pos, transform.rotation);
                _player._money -= 5;
                _uiManager.UpdateMoney(-5);
            }
        }
        if (_player._money >= 10)
        {
            if (_player._Type == Player.Type.Ice)
            {
                GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
                turret = (GameObject)Instantiate(turretToBuild, transform.position + _pos, transform.rotation);
                _player._money -= 10;
                _uiManager.UpdateMoney(-10);
            }
        }
        if (_player._money >= 15)
        {
            if (_player._Type == Player.Type.Fire)
            {
                GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
                turret = (GameObject)Instantiate(turretToBuild, transform.position + _pos, transform.rotation);
                _player._money -= 15;
                _uiManager.UpdateMoney(-15);
            }
        }

    }

    void OnMouseEnter()
    {
        _render.material.color = _hoverColor;
    }

    void OnMouseExit()
    {
        _render.material.color = _startColor;
    }
}
