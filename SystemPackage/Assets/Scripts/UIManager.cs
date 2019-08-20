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
    public Slider HPBar;
    public float _currentHP;
    private int _scoreValue;
    public int _waves = 0;
    public int _money;
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
        _money = 0;
        _waveText.text = "";
        _moneyText.text = "";
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
        _money += money;
        _moneyText.text = "$ " + _money.ToString();
    }

    public void UpdateWaves(int wave)
    {
        _waves += wave;
        _waveText.text = _waves.ToString();
    }
}
