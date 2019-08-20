﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : AsyncLoader
{
    public int sceneIndexToLoad = 1;
    public LoadingScreen loadingScreen = null;
    private static int _sceneIndex = 1;
    private static GameLoader _instance; // The only singleton you should have.
    public GameObject UIManagerPrefab = null;
    // All of the components that implement the IGameModule interface.
    public List<Component> gameModules = new List<Component>();

    protected override void Awake()
    {
        Debug.Log("GameLoader Starting");

        // Saftey check
        if (_instance != null && _instance != this)
        {
            Debug.Log("A duplicate instance of the GameLoader was found, and will be ignored. Only one instance is permitted");
            Destroy(gameObject);
            return;
        }

        // Set reference to this instance
        _instance = this;

        // Make persistent
        DontDestroyOnLoad(gameObject);

        // Scene Index Check
        if (sceneIndexToLoad < 0 || sceneIndexToLoad >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Invalid Scene Index {sceneIndexToLoad} ... using default value of {_sceneIndex}");
        }
        else
        {
            _sceneIndex = sceneIndexToLoad;
        }

        // Setup System GameObject
        GameObject systemsGO = new GameObject("[Services]");
        systemsGO.tag = "Services";
        Transform systemsParent = systemsGO.transform;
        DontDestroyOnLoad(systemsGO);

        loadingScreen.UpdateLoadingStep("Loading Game Systems");

        // Queue up loading routines
        Enqueue(IntializeCoreSystems(systemsParent), 50, UpdateCoreSystemsProgress);
        Enqueue(InitializeModularSystems(systemsParent), 50, UpdateModularSystemsProgress);

        // Set completion callback
        CallOnComplete(OnComplete);
    }

    private float _coreLoadTotalSteps = 10.0f;
    private float _coreLoadCurrentStep = 0.0f;

    private float UpdateCoreSystemsProgress()
    {
        return _coreLoadCurrentStep / _coreLoadTotalSteps;
    }

    private float _modularLoadTotalSteps = 10.0f;
    private float _modularLoadCurrentStep = 0.0f;
    private float UpdateModularSystemsProgress()
    {
        return _modularLoadCurrentStep / _modularLoadTotalSteps;
    }

    protected override void ProgressUpdated(float percentComplete)
    {
        base.ProgressUpdated(percentComplete);
        loadingScreen.UpdateLoadingBar(percentComplete);
        Debug.Log("Progress: " + percentComplete * 100.0f);
    }

    private IEnumerator IntializeCoreSystems(Transform systemsParent)
    {
        // Setup Core Systems
        Debug.Log("Loading Core Systems");
        yield return new WaitForSeconds(7.0f);

        // UI Manager
        GameObject uiManagerGO = GameObject.Instantiate(UIManagerPrefab);
        uiManagerGO.transform.SetParent(systemsParent);
        uiManagerGO.SetActive(false);
        UIManager uiManagerComp = uiManagerGO.GetComponent<UIManager>();
        ServiceLocator.Register<UIManager>(uiManagerComp.Init());

        // GameManager
        GameObject gameManagerGO = new GameObject("GameManager");
        gameManagerGO.transform.SetParent(systemsParent);
        var gameManagerComp = gameManagerGO.AddComponent<GameManager>();
        gameManagerComp = gameManagerGO.AddComponent<GameManager>();
        ServiceLocator.Register<GameManager>(gameManagerComp.Initialize(_sceneIndex));

        for (int i = 0; i < 8; i++)
        {
            _coreLoadCurrentStep += 1.0f;
            yield return null;
        }
    }

    private IEnumerator InitializeModularSystems(Transform systemsParent)
    {
        // Setup Additional Systems as needed
        Debug.Log("Loading Modular Systems");
        foreach(var module in gameModules)
        {
            if(module is IGameModule)
            {
                IGameModule gameModule = module as IGameModule;
                yield return gameModule.LoadModule();
            }
        }
    }

    private void OnComplete()
    {
        Debug.Log("GameLoader Completed");
        StartCoroutine(LoadInitialScene(_sceneIndex));
    }

    private IEnumerator LoadInitialScene(int index)
    {
        Debug.Log("GameLoader Starting Scene Load");
        var loadOp = SceneManager.LoadSceneAsync(index);
        
        loadingScreen.UpdateLoadingStep("Loading Scene: " + index.ToString());
        
        while (!loadOp.isDone)
        {
            loadingScreen.UpdateLoadingBar(loadOp.progress);
            yield return loadOp;
        }
    }
}