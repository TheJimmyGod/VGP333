using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public enum Type
    {
        Basic,
        Fire,
        Ice
    }
    public Type _Type;
    private UIManager _uiManager = null;
    public int _money = 15;
    public float _life;
    public GameObject _playerObj;

    void Awake()
    {
        ServiceLocator.Register<Player>(this);
        if(this.gameObject == null)
        {
            _playerObj = GameObject.FindGameObjectWithTag("Player");
            _playerObj = ServiceLocator.Get<Player>().gameObject;
        }
        _uiManager = ServiceLocator.Get<UIManager>();
        _uiManager.UpdatePlayerScore(0);
        _uiManager.UpdatePlayerHP(100);
        _uiManager.SetTower(1);
        _Type = Type.Basic;
        _money = _money + 5 * ServiceLocator.Get<GameManager>()._sceneIndexToLoad;
        _uiManager.UpdateMoney(_money);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _Type = Type.Basic;
            _uiManager.SetTower(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _Type = Type.Fire;
            _uiManager.SetTower(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _Type = Type.Ice;
            _uiManager.SetTower(3);
        }
        if (_life <= 0)
            Destroy(this);
    }
}
