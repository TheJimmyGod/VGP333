using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int _score = -1;
    public int _waves = 0;
    public const string SCORE_KEY = "Score";
    public const string PLAYER_WAVE = "Wave";
    private void Awake()
    {
        GameLoader.CallOnComplete(Init);
    }
    private void Init()
    {
        ServiceLocator.Register<PlayerData>(this);
        _score = PlayerPrefs.GetInt(SCORE_KEY, 0);
        _waves = PlayerPrefs.GetInt(PLAYER_WAVE, 0);
    }

    public void SavePlayerData(int score, int wave)
    {
        SaveScore(score);
        SaveWave(wave);
    }

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(SCORE_KEY, score);
    }

    public void SaveWave(int wave)
    {
        PlayerPrefs.SetInt(PLAYER_WAVE, wave);
    }
}
