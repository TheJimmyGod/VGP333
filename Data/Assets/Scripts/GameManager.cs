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

    public void UpdateObjectives()
    {
        ++requiredTowin;
        _uiManager.UpdatePlayerObject(requiredTowin);
    }

    void CheckPlayerWin()
    {

        if (SceneManager.GetActiveScene().buildIndex < 2 && requiredTowin == 3)
        {
            requiredTowin = 0;
            _uiManager.UpdatePlayerObject(0);
            SetSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2 && requiredTowin == 3)
        {
            _uiManager.SetWinText();
        }
    }
    void CheckPlayerLose()
    {
        
        if (ServiceLocator.Get<Player>().CurrentHealth <= 0)
        {
            ServiceLocator.Get<UIManager>().winloseText.text = "You lose!";
        }
    }
}
