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
    public GameManager Initialize(int index)
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        SetSceneIndex(index);
        return this;
    }

    void Update()
    {
        CheckPlayerWin();
        CheckPlayerLose();
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

    public void UpdatePlayerCurrentBullet(int delta)
    {
        _uiManager.UpdateCurrentBulletCount(delta);
    }

    public void UpdatePlayerTotalBullet(int delta)
    {
        _uiManager.UpdateTotalBulletCount(delta);
    }

    void CheckPlayerWin()
    {
        //int requiredTowin;
        if(_currentScore > 100)
        {
            _uiManager.SetWinText();
            //SetSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void CheckPlayerLose()
    {
        if (_currentHP <= 0)
        {
            _uiManager.winloseText.text = "You lose!";
        }
    }
}
