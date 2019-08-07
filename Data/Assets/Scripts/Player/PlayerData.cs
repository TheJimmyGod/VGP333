using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int _score = -1;
    private string _name = string.Empty;
    public const string SCORE_KEY = "PlayerScore";
    public const string PLAYER_NAME = "PlayerName";
    private void Awake()
    {
        GameLoader.CallonComplete(Init);
    }
    private void Init()
    {
        ServiceLocator.Register<PlayerData>(this);
        _score = PlayerPrefs.GetInt(SCORE_KEY, 0);
        _name = PlayerPrefs.GetString(PLAYER_NAME, "");
        Core.Debug.Log($"Player Score {_score.ToString()} : Player Name:{_name}");
    }

    public void SavePlayerData(int score, string name)
    {
        SaveScore(score);
        SavePlayerName(name);
    }

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(SCORE_KEY, score);
    }

    public void SavePlayerName(string name)
    {
        PlayerPrefs.SetString(PLAYER_NAME, name);
    }
}
