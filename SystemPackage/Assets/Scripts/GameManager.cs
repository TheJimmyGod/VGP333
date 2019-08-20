using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager = null;
    public int _currentScore;
    private int _sceneIndexToLoad = 0;
    private int _currentBullet;
    private int _totalBullet;
    private float _currentHP = 100.0f;
    public int requiredTowin = 0;
    private int _wave = 0;
    public GameManager Initialize(int index)
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        SetSceneIndex(index);
        return this;
    }

    void Update()
    {
        CheckPlayerWinLose();
    }

    public void SetSceneIndex(int index)
    {
        _sceneIndexToLoad = index;
    }

    public void UpdatePlayerHP()
    {
        _currentHP = _uiManager._currentHP;
    }

    public void UpdateScore(int delta)
    {
        _currentScore += delta;
        _uiManager.UpdatePlayerScore(_currentScore);
    }

    public void CheckWaves()
    {
        _wave = _uiManager._waves;
    }

    void CheckPlayerWinLose()
    {
        if (_wave >= 4)
        {
            _uiManager.SetWinText();
        }
        if (_currentHP <= 0)
        {
            _uiManager.SetLoseText();
        }
    }
}
