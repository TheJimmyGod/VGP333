using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager = null;
    public int mGenerateCount = 0;
    private int _currentScore;
    private int _sceneIndexToLoad = 0;
    public int CurrentScore { get { return _currentScore; } set { _currentScore = value; } }
    public GameManager Initialize(int index)
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        SetSceneIndex(index);
        return this;
    }

    void FixedUpdate()
    {
        CheckPlayerWin();
        CheckPlayerLose();
    }

    public void SetSceneIndex(int index)
    {
        _sceneIndexToLoad = index;
    }

    public void UpdateScore(int delta)
    {
        _currentScore += delta;
        _uiManager.UpdateObjectCount(_currentScore);
    }

    void CheckPlayerWin()
    {
        //int requiredTowin;
        if(CurrentScore > 100)
        {
            _uiManager.SetWinText();
            //SetSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void CheckPlayerLose()
    {
        //if(mPlayer.pos.y < -4.0f)
        //{
        //    Destroy(mPlayer);
        //}
        //if(mPlayer == null)
        //{
        //    _uiManager.winloseText.text = "You win!";
        //}
    }
}
