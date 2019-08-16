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

    private int _currentWave = 0;
    private List<GameObject> _activeEnemies = new List<GameObject>();
    private WayPointManager.Path _path;
    private Action OnDeath;     // TODO - Add an OnDeath action

    public void OnKilled()
    {
        _activeEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void Awake()
    {
        OnDeath += OnKilled;
        if (UnitPrefeb == null)
        {
            Debug.LogError("UnitSpawner disabled: Unit Prefab is NULL");
            gameObject.SetActive(false);
            return;
        }
        OnDestroy();
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
        while(_currentWave < numberOfWaves)
        {
            SpawnWave(_currentWave);
            yield return new WaitForSeconds(secondBetweenWave);
        }
    }

    private void SpawnWave(int waveNumber)
    {
        for (int i = 0; i < enemiesPerWave; ++i)
        {
            GameObject enemy = GameObject.Instantiate(UnitPrefeb, transform.position, transform.rotation);
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().Initialize(_path, OnDeath); // TODO - pass the on killed action
            _activeEnemies.Add(enemy);
        }
    }

    private void OnDestroy()
    {
        OnDeath -= OnKilled;
    }
}
