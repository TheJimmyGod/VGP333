using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject _prefeb;
    public string _name = string.Empty;
    public int _score = 0;
    public float _exp = 0.0f;
    public float _damage = 0.0f;
    public float _speed = 0.0f;
    public float _maxHP = 0.0f;
    public void SavePlayerData()
    {
        _name = ServiceLocator.Get<PlayerController>()._name;
        _speed = ServiceLocator.Get<PlayerController>()._speed;
        _maxHP = ServiceLocator.Get<PlayerController>()._maxHP;
        _damage = ServiceLocator.Get<PlayerController>()._damage;
        _exp = ServiceLocator.Get<PlayerController>()._exp;
        _score = ServiceLocator.Get<GameManager>()._currentScore;
        ServiceLocator.Get<PlayerData>().SavePlayerData(_maxHP,_speed,_damage,_exp,_name,_score);
    }

    private void Awake()
    {
        ServiceLocator.Register<LevelManager>(this);
        ServiceLocator.Get<UIManager>().gameObject.SetActive(true);
        if(ServiceLocator.Get<PlayerController>() == null)
        {
            GameObject _character = GameObject.Instantiate(_prefeb);
            _character.transform.position = this.transform.position;
        }
    }
}
