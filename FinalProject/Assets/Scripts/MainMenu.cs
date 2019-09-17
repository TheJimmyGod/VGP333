using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int levelToLoad = 1;
    private GameManager _GO = null;
    public void OnLevelButtonClick(int level)
    {
        levelToLoad = level;
        _GO.SetSceneIndex(level);
        StartCoroutine(LoadLevelRoutine());
    }

    void Awake()
    {
        _GO = ServiceLocator.Get<GameManager>();
        _GO.SetSceneIndex(levelToLoad);
        _GO.SetState(GameManager.GamePlay.MainMenu);
    }

    private IEnumerator LoadLevelRoutine()
    {
        yield return SceneManager.LoadSceneAsync(levelToLoad);
    }
}
