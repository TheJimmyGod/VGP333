using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GamePlay
    {
        MainMenu,
        OnPlaying,
        Boss,
        GameOver
    }

    public GamePlay _state;
    private LevelManager _levelManager = null;
    private PlayerController _player = null;
    private UIManager _uiManager = null;
    public int _currentScore;
    public float _exp = 0;
    public int _sceneIndexToLoad = 0;
    private int _currentBullet;
    private int _totalBullet;
    private float _currentHP = 100.0f;
    public int _level = 1;
    public int _requiredToWin = 0;
    public bool _isBossKilled = false;
    public GameManager Initialize(int index)
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        _player = ServiceLocator.Get<PlayerController>();
        _levelManager = ServiceLocator.Get<LevelManager>();
        SetSceneIndex(index);
        Checklevels();
        _state = GamePlay.MainMenu;
        return this;
    }

    void Update()
    {
        if(_uiManager == null)
        {
            Debug.Log("Nuh");
        }
        CheckPlayerWin();
        SetSceneIndex(SceneManager.GetActiveScene().buildIndex);
        if(_state == GamePlay.OnPlaying)
        {
            if (_player == null)
            {
                _player = ServiceLocator.Get<PlayerController>();
            }
            else
            {
                if (_player._exp >= 100.0)
                {
                    _level++;
                    _player._exp = 0;
                    _player._maxHP += 20.0f;
                    _player._speed += 0.05f;
                    _player._damage += 5.0f;
                    _player._currHP = _player._maxHP;
                    _uiManager.HPBar.maxValue = _player._maxHP;
                    _uiManager.HPBar.value = _currentHP;
                }

            }
        }
        else if(_state == GamePlay.GameOver)
        {
            SetPlayerLose();
            Destroy(_player.gameObject);
        }
    }

    public void SetSceneIndex(int index)
    {
        _sceneIndexToLoad = index;
        if(_sceneIndexToLoad >= 3)
        {
            SetState(GamePlay.OnPlaying);
        }
    }

    public void UpdatePlayerHP(float dmg)
    {
        _currentHP -= dmg;
        _uiManager.HPBar.value = _currentHP;
    }

    public void UpdateScore(int delta)
    {
        _currentScore += delta;
        _uiManager.UpdatePlayerScore(_currentScore);
    }

    public void UpdateEXP(float delta)
    {
        _exp += delta;
        _uiManager.UpdateEXP(_exp);
    }

    public void Checklevels()
    {
        _uiManager._level = _level;
    }

    public void SetState(GamePlay state)
    {
        _state = state;
    }

    void CheckPlayerWin()
    {
        if (_isBossKilled)
        {
            _uiManager.SetWinText();
        }
        if (_requiredToWin >= 10 && _sceneIndexToLoad <= 3)
        {
            Debug.Log("Next ON!");
 
            ServiceLocator.Get<PlayerData>().SavePlayerData(_player._maxHP, _player._speed,_player._damage,_player._exp,_player._name,_currentScore, _level);
            SetSceneIndex(_sceneIndexToLoad + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if(_levelManager == null)
            {
                _levelManager = ServiceLocator.Get<LevelManager>();
                _levelManager.LoadPlayerData();
            }
            else
            {
                _levelManager.LoadPlayerData();
            }
            _requiredToWin = 0;
        }
    }

    public void SetPlayerLose()
    {
        _uiManager.SetLoseText();
    }
}
