using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text winloseText;
    public Text _waveText;
    public Text _moneyText;
    public Text _score;
    public Text _TName;
    public Text _TDmg;
    public Text _TCost;
    public Slider HPBar;
    public float _currentHP;
    public int _scoreValue;
    public int _waves = 0;
    public int _Tower;
    private const string WIN_MESSAGE = "You Win!!";
    private const string LOSE_MESSAGE = "You Lose!!";

    private void Awake()
    {
        Debug.Log("UI Manager Initializing");
        Init();
    }

    public UIManager Init()
    {
        winloseText.text = "";
        _score.text = "";
        HPBar.value = 100.0f;
        _currentHP = 100.0f;
        _scoreValue = 0;
        _waves = 0;
        _waveText.text = "";
        _moneyText.text = "";
        _Tower = 1;
        return this;
    }

    public void SetWinText()
    {
        winloseText.text = WIN_MESSAGE;
    }
    
    public void SetLoseText()
    {
        winloseText.text = LOSE_MESSAGE;
    }

    public void UpdatePlayerScore(int score)
    {
        _scoreValue += score;
        _score.text = _scoreValue.ToString();
    }

    public void UpdatePlayerHP(float _current)
    {
        HPBar.value = _current;
        _currentHP = _current;
    }

    public void UpdateMoney(int money)
    {
        _moneyText.text = "$ " + money.ToString();
    }

    public void UpdateWaves(int wave)
    {
        _waves += wave;
        _waveText.text = _waves.ToString();
    }

    public void SetTower(int num)
    {
        switch(num)
        {
            case 1:
                _TName.text = "Tower : Basic Tower";
                _TDmg.text = "Damage : 5";
                _TCost.text = "Cost : $5";
                break;
            case 2:
                _TName.text = "Tower : Fire Tower";
                _TDmg.text = "Damage : 25";
                _TCost.text = "Cost : $15";
                break;
            case 3:
                _TName.text = "Tower : Ice Tower";
                _TDmg.text = "Damage : 3";
                _TCost.text = "Cost : $10";
                break;
        }
    }
}
