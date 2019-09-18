using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text winloseText;
    public Text _levelText;
    public Text _expText;
    public Text _score;
    public Slider HPBar;
    public float _currentHP;
    public int _scoreValue;
    public int _exp = 0;
    public int _level = 1;
    public GameManager _GO;
    private const string WIN_MESSAGE = "You Win!!";
    private const string LOSE_MESSAGE = "You Lose!!";

    private void Awake()
    {
        Debug.Log("UI Manager Initializing");
        Init();
    }

    public UIManager Init()
    {
        _GO = ServiceLocator.Get<GameManager>();
        winloseText.text = "";
        _score.text = "";
        HPBar.value = 0.0f;
        _currentHP = 0.0f;
        _scoreValue = 0;
        _exp = 0;
        _level = 1;
        _levelText.text = "1";
        _expText.text = "EXP ";

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
        _scoreValue = score;
        _score.text = _scoreValue.ToString();
    }

    public void UpdatePlayerHP(float _current)
    {
        HPBar.value = _current;
        _currentHP = _current;
    }

    public void UpdateEXP(float exp)
    {
        _expText.text = "EXP " + exp.ToString();
    }

    public void UpdateLevel()
    {
        _levelText.text = _level.ToString();
    }

    public void Reset()
    {
        winloseText.text = "";
        _score.text = "";
        HPBar.value = 100.0f;
        _currentHP = 0.0f;
        _scoreValue = 0;
        _exp = 0;
        _level = 1;
        _levelText.text = "1";
        _expText.text = "EXP ";
    }
}
