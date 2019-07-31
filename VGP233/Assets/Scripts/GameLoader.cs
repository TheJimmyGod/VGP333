using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : AsyncLoader
{
    public int sceneIndexToload = 1;
    private static int _sceneIndex = 1;
    private static GameLoader _instance; // The only singleton you should have.
    protected override void Awake()
    {
        Debug.Log("GameLoader Starting");

        // Safety Check
        if(_instance != null && _instance != this)
        {
            Debug.Log("A duplicate instance of the GameLoader was found");
            Destroy(gameObject);
            return;
        }

        _instance = this; // Singleton

        DontDestroyOnLoad(gameObject);

        // Scene Index Check
        if(sceneIndexToload < 0 || sceneIndexToload > SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Given scene index is invalid");
            _sceneIndex = 1;
        }
        else
        {
            _sceneIndex = sceneIndexToload;
        }

        // Setup Systems GameObject
        GameObject systemsGO = new GameObject("[Services]");
        systemsGO.tag = "Services";
        Transform systemsParent = systemsGO.transform;
        DontDestroyOnLoad(systemsGO);

        // Queue up loading routines
        Enqueue(InitializeCoreSystems(systemsParent), 70);
        Enqueue(InitializingModularSystem(systemsParent), 30);
        // Set the completion callback

        CallonComplete(OnComplete);

    }

    private IEnumerator InitializeCoreSystems(Transform systemsParent)
    {
        Debug.Log("Initializing Core System");
        yield return new WaitForSeconds(7.0f);

        GameObject uiManagerGO = new GameObject("UIManager");
        uiManagerGO.transform.SetParent(systemsParent);
        UIManager uiManagerComp = uiManagerGO.AddComponent<UIManager>();
        ServiceLocator.Register<UIManager>(uiManagerComp);
    }

    private IEnumerator InitializingModularSystem(Transform systemsParent)
    {
        Debug.Log("Initializing Modular System");
        yield return new WaitForSeconds(3.0f);
    }

    private void OnComplete()
    {
        StartCoroutine(LoadInitialScene(_sceneIndex));
    }
    
    private IEnumerator LoadInitialScene(int index)
    {
        Debug.Log("GameLoader Starting Scene Load");
        yield return SceneManager.LoadSceneAsync(index);
    }
}
