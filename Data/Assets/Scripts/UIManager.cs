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
    public Slider HPBar;
    public float _currentHP;
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
    
    public void UpdatePlayerScore(int score)
    {
        _score.text = score.ToString();
    }

    public void UpdatePlayerHP(float _current)
    {
        HPBar.value = _current;
        _currentHP = _current;
    }
}
