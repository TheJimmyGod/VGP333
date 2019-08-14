using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Need hud;
    private Canvas canvas;
    public Text winloseText;
    public Text _currentBullet;
    public Text _totalBullet;
    public Text _score;
    public Text _nameofGun;
    public Text _requiredObj;
    public Slider HPBar;
    public int _requiredObjValue;
    public float _currentHP;
    private int _scoreValue;
    private const string WIN_MESSAGE = "You Win!!";

    private void Awake()
    {
        Debug.Log("UI Manager Initializing");
        Init();
    }

    public UIManager Init()
    {
        winloseText.text = "";
        _currentBullet.text = "";
        _totalBullet.text = "";
        _score.text = "";
        HPBar.value = 100.0f;
        _currentHP = 100.0f;
        _requiredObj.text = "";
        _nameofGun.text = "";
        _scoreValue = 0;
        return this;
    }

    public void SetWinText()
    {
        winloseText.text = WIN_MESSAGE;
    }

    public void UpdateCurrentBulletCount(float count)
    {
        _currentBullet.text = count.ToString();
    }

    public void UpdateTotalBulletCount(float count)
    {
        _totalBullet.text = count.ToString();
    }

    public void UpdateGunText(string text)
    {
        _nameofGun.text = text;
    }
    
    public void UpdatePlayerScore(int score)
    {
        _scoreValue += score;
        _score.text = _scoreValue.ToString();
    }

    public void UpdatePlayerObject(int obj)
    {
        _requiredObj.text = obj.ToString();
        _requiredObjValue = obj;
    }

    public void UpdatePlayerHP(float _current)
    {
        HPBar.value = _current;
        _currentHP = _current;
    }
}
