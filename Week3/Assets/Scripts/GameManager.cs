using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text mScore;
    public Text mGenerate;
    public Text mWinAndLose;
    public Slider HPbar;
    public Player mPlayer;
    public GameObject door;
    public int mGenerateCount = 0;

    private void Awake()
    {
        mScore.text = "Count : " + mPlayer.mCount.ToString();
        mGenerate.text = "Generate(%) : " + mGenerateCount.ToString();
        mWinAndLose.text = "";
        HPbar.value = mPlayer._currentHealth;
        Debug.Log("Hello!");
    }

    void FixedUpdate()
    {
        CheckPlayerWin();
        CheckPlayerLose();
        CheckCountText();
        if (mPlayer.isActive)
        {
            if (mGenerateCount < 100)
            {
                mGenerateCount++;
            }
            else if (mGenerateCount == 100)
            {
                Destroy(door);
            }
        }
        HPbar.value = mPlayer._currentHealth;
    }

    void CheckPlayerWin()
    {
        if(mPlayer.mCount == 10)
        {
            mWinAndLose.text = "You win!";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void CheckPlayerLose()
    {
        if(mPlayer.pos.y < -4.0f)
        {
            Destroy(mPlayer);
        }
        if(mPlayer == null)
        {
            mWinAndLose.text = "You lose!";
        }
    }

    void CheckCountText()
    {
        mScore.text = "Count : " + mPlayer.mCount.ToString();
        mGenerate.text = "Generate(%) : " + mGenerateCount.ToString();

    }
}
