using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    private UIManager _uiManager = null;
    public int _currentScore;
    public int _requiredWin = 0;
    public int _sceneIndexToLoad = 0;
    private int _currentBullet;
    private int _totalBullet;
    private float _currentHP = 100.0f;
    public int _wave = 0;

    public GameManager Initialize(int index)
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        SetSceneIndex(index);
        return this;
    }

    void Update()
    {
        CheckPlayerWinLose();
        SetSceneIndex(SceneManager.GetActiveScene().buildIndex);
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

    public void UpdateRequireToWin(int delta)
    {
        _requiredWin += delta;
    }

    public void CheckWaves()
    {
        _wave = _uiManager._waves;
    }

    void CheckPlayerWinLose()
    {
        
        //if (_wave >= 3 && _requiredWin == 25)
        //{
        //    _uiManager.SetWinText();
        //}
        //if (_requiredWin == 25 && _wave < 3)
        //{
        //    _requiredWin = 0;
        //    ServiceLocator.Get<PlayerData>().SavePlayerData(_uiManager._scoreValue, _uiManager._waves);
        //    SetSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
        //if (_currentHP <= 0)
        //{
        //    _uiManager.SetLoseText();
        //}
    }
}
