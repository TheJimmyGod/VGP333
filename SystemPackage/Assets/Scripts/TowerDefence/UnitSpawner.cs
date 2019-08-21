using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour

{
    public GameObject UnitPrefeb;
    public int numberOfWaves;
    public int enemiesPerWave;
    public int secondBetweenWave;
    public int secondsStartDelay;
    public int pathId;
    public UIManager _uiManager;
    private int _currentWave = 0;
    public List<GameObject> _activeEnemies = new List<GameObject>();
    private WayPointManager.Path _path;
    private Action OnDeath;     // TODO - Add an OnDeath action
    public void OnKilled()
    {
        Debug.Log("Active");
        for (int i = 0; i < _activeEnemies.Count; ++i)
        {
            if(_activeEnemies[i] == null)
            {
                _activeEnemies.Remove(_activeEnemies[i]);
            }
        }

    }


    private void Awake()
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        _uiManager.UpdateWaves(1);
        OnDeath += OnKilled;
        if (UnitPrefeb == null)
        {
            Debug.LogError("UnitSpawner disabled: Unit Prefab is NULL");
            gameObject.SetActive(false);
            return;
        }
    }

    public void Init(WayPointManager.Path path, System.Action onEnemnyKilled = null)
    {
        _path = path;
        OnDeath += onEnemnyKilled;
        // TODO - assign on killed action
    }

    public void StartSpawner()
    {
        StartCoroutine("BeginWaveSpawn");
    }

    private IEnumerator BeginWaveSpawn()
    {
        yield return new WaitForSeconds(secondsStartDelay);
        while (_currentWave < numberOfWaves)
        {
            SpawnWave(_currentWave);
            _currentWave++;
            yield return new WaitForSeconds(secondBetweenWave);
        }
        while(_activeEnemies.Count <= 1)
        {

        }
        _uiManager.SetWinText();
    }

    private void SpawnWave(int waveNumber)
    {
        for (int i = 0; i < enemiesPerWave; ++i)
        {
            GameObject enemy = GameObject.Instantiate(UnitPrefeb, transform.position, transform.rotation);
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().Initialize(_path, Recycle); // TODO - pass the on killed action
            _activeEnemies.Add(enemy);
        }
    }

    private void Recycle(GameObject obj)
    {
        ServiceLocator.Get<ObjectPoolManager>().Recycle(obj);
    }
    void OnDestroy()
    {
        OnDeath -= OnKilled;
    }
}
