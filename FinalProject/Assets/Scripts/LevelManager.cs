using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject _prefeb;
    private System.Action OnRecycle;
    public string _name = string.Empty;
    public int _score = 0;
    public float _exp = 0.0f;
    public float _damage = 0.0f;
    public float _speed = 0.0f;
    public float _maxHP = 0.0f;
    public int _level = 1;
    public void SavePlayerData()
    {
        _name = ServiceLocator.Get<PlayerController>()._name;
        _speed = ServiceLocator.Get<PlayerController>()._speed;
        _maxHP = ServiceLocator.Get<PlayerController>()._maxHP;
        _damage = ServiceLocator.Get<PlayerController>()._damage;
        _exp = ServiceLocator.Get<PlayerController>()._exp;
        _score = ServiceLocator.Get<GameManager>()._currentScore;
        _level = ServiceLocator.Get<GameManager>()._level;
        ServiceLocator.Get<PlayerData>().SavePlayerData(_maxHP,_speed,_damage,_exp,_name,_score,_level);
    }

    public void LoadPlayerData()
    {
        ServiceLocator.Get<PlayerController>()._name = ServiceLocator.Get<PlayerData>()._name;
        ServiceLocator.Get<PlayerController>()._damage = ServiceLocator.Get<PlayerData>()._damage;
        ServiceLocator.Get<PlayerController>()._speed = ServiceLocator.Get<PlayerData>()._speed;
        ServiceLocator.Get<PlayerController>()._maxHP = ServiceLocator.Get<PlayerData>()._maxHP;
        ServiceLocator.Get<PlayerController>()._exp = ServiceLocator.Get<PlayerData>()._exp;
        ServiceLocator.Get<GameManager>()._level = ServiceLocator.Get<PlayerData>()._level;

    }

    private void Awake()
    {
        ServiceLocator.Register<LevelManager>(this);
        ServiceLocator.Get<UIManager>().gameObject.SetActive(true);
    }
}
