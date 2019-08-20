using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Awake()
    {
        ServiceLocator.Register<Player>(this);
        _uiManager = ServiceLocator.Get<UIManager>();
        _uiManager.UpdateMoney(15);
        _uiManager.UpdatePlayerScore(0);
        _uiManager.UpdatePlayerHP(100);
        _uiManager.UpdateWaves(0);
        _Type = Type.Basic;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _Type = Type.Basic;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _Type = Type.Fire;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _Type = Type.Ice;
        if (_life <= 0)
            Destroy(this);
    }
}
