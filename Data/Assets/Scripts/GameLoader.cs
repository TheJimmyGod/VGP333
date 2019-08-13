using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

using Debug = Core.Debug;

public class GameLoader : AsyncLoader
{
    public int sceneIndexToload = 1;
    private static int _sceneIndex = 1;
    private static GameLoader _instance; // The only singleton you should have.
    private LoadingScreen loadingScreen;

    public GameObject UIManagerPrefab = null;

    // All of the components that implement the IGamemodule interface
    public List<Component> gameModules = new List<Component>();

    private float _coreLoadTotalSteps = 50.0f;
    private float _coreLoadCurrentStep = 1.0f;
    private float _modularLoadTotalSteps = 50.0f;
    private float _modularLoadCurrentStep = 1.0f;

    private float UpdateCoreSystemsProgress()
    {
        return _coreLoadCurrentStep / _coreLoadTotalSteps;
    }

    private float UpdateModularSystemsProgress()
    {
        return _modularLoadCurrentStep / _modularLoadTotalSteps;
    }

    protected override void ProgressUpdated(float percentComplete)
    {
        base.ProgressUpdated(percentComplete);
        loadingScreen.UpdateLoadingBar(percentComplete);
        Debug.Log("ProgressL " + percentComplete * 100.0f);
    }

    protected override void Awake()
    {
        loadingScreen = GetComponent<LoadingScreen>();
        loadingScreen.UpdateLoadingStep("GameLoader Starting");

        // Safety Check
        if (_instance != null && _instance != this)
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

        loadingScreen.UpdateLoadingStep("Loading Game Systems");

        // Queue up loading routines
        Enqueue(InitializeCoreSystems(systemsParent), 50, UpdateCoreSystemsProgress);
        Enqueue(InitializingModularSystem(systemsParent), 50, UpdateModularSystemsProgress);
        // Set the completion callback

        CallonComplete(OnComplete);

    }

    private IEnumerator InitializingModularSystem(Transform systemsParent)
    {
        loadingScreen.UpdateLoadingStep("Loading Modular Systems");
        yield return new WaitForSeconds(2.0f);
        foreach (var module in gameModules)
        {
            if(module is IGameModule)
            {
                IGameModule gameModule = module as IGameModule;
                yield return gameModule.LoadModule();
            }
        }
    }
    private IEnumerator InitializeCoreSystems(Transform systemsParent)
    {
        loadingScreen.UpdateLoadingStep("Initializing Core System");
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

    private void OnComplete()
    {
        loadingScreen.UpdateLoadingStep("GameLoader Completed");
        ServiceLocator.Get<GameManager>()._currentScore = PlayerPrefs.GetInt("PlayerScore", 0);
        StartCoroutine(LoadInitialScene(_sceneIndex));
    }
    
    private IEnumerator LoadInitialScene(int index)
    {
        loadingScreen.UpdateLoadingStep("GameLoader Starting Scene Load");
        yield return new WaitForSeconds(2.0f);
        var loadOp = SceneManager.LoadSceneAsync(index);

        loadingScreen.UpdateLoadingStep("Loading Scene: " + index.ToString());
        while(!loadOp.isDone)
        {
            loadingScreen.UpdateLoadingBar(loadOp.progress);
            yield return loadOp;
        }
    }
}
