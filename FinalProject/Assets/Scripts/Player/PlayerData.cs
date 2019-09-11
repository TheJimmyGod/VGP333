using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int _score = 0;
    public float _exp = 0.0f;
    public float _damage = 0.0f;
    public float _speed = 0.0f;
    public float _maxHP = 0.0f;
    public string _name = string.Empty;
    public const string SCORE_KEY = "Score";
    public const string EXP_KEY = "Exp";
    public const string DAMAGE_KEY = "Damage";
    public const string SPEED_KEY = "Speed";
    public const string MAXHP_KEY = "MaxHP";
    public const string NAME_KEY = "Name";


    private void Awake()
    {
        GameLoader.CallOnComplete(Init);
    }

    private void Init()
    {
        ServiceLocator.Register<PlayerData>(this);
        _score = PlayerPrefs.GetInt(SCORE_KEY, 0);
        _exp = PlayerPrefs.GetFloat(EXP_KEY, 0.0f);
        _damage = PlayerPrefs.GetFloat(DAMAGE_KEY, 0.0f);
        _maxHP = PlayerPrefs.GetFloat(MAXHP_KEY, 0.0f);
        _speed = PlayerPrefs.GetFloat(SPEED_KEY, 0.0f);
        _name = PlayerPrefs.GetString(NAME_KEY, string.Empty);
    }

    public void SavePlayerData(float maxHP, float speed, float dmg, float exp,
        string name, int score)
    {
        SaveMaxHP(maxHP);
        SaveExp(exp);
        SaveScore(score);
        SaveDamage(dmg);
        SaveName(name);
        SaveSpeed(speed);
    }

    public void SaveName(string n)
    {
        PlayerPrefs.SetString(NAME_KEY, n);
    }

    public void SaveExp (float e)
    {
        PlayerPrefs.SetFloat(EXP_KEY,e);
    }
    
    public void SaveDamage(float d)
    {
        PlayerPrefs.SetFloat(DAMAGE_KEY, d);
    }

    public void SaveMaxHP(float h)
    {
        PlayerPrefs.SetFloat(MAXHP_KEY, h);
    }

    public void SaveSpeed(float s)
    {
        PlayerPrefs.SetFloat(SPEED_KEY, s);
    }

    public void SaveScore(int s)
    {
        PlayerPrefs.SetInt(SCORE_KEY, s);
    }
}
