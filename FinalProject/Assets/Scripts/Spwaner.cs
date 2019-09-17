using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spwaner : MonoBehaviour
{

    public GameObject UnitPrefeb;
    public int numberOfWaves;
    public int enemiesPerWave;
    public int secondBetweenWave;
    public int secondsStartDelay;

    public UIManager _uiManager;
    private int _currentWave = 0;
    public List<GameObject> _activeEnemies = new List<GameObject>();
    private Action OnRecycle;
    private GameObject _go;
    private Action OnDeath;
    private void Awake()
    {
        _uiManager = ServiceLocator.Get<UIManager>();
        if (UnitPrefeb == null)
        {
            Debug.LogError("UnitSpawner disabled: Unit Prefab is NULL");
            gameObject.SetActive(false);
            return;
        }
    }

    public void Init(Action onEnemnyKilled = null)
    {
        OnDeath += onEnemnyKilled;
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
    }

    void Update()
    {
        foreach (GameObject objects in _activeEnemies)
        {
            if(objects == null)
            {
                _activeEnemies.Remove(objects);
            }
        }
    }

    private void SpawnWave(int waveNumber)
    {
        for (int i = 0; i < enemiesPerWave; ++i)
        {
            //GameObject enemy = GameObject.Instantiate(UnitPrefeb, transform.position, transform.rotation);
            GameObject enemy = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(UnitPrefeb.name);
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
            OnRecycle = () => Recycle(enemy);
            enemy.GetComponent<Enemy>().Initialize(OnRecycle); // TODO - pass the on killed action
            _activeEnemies.Add(enemy);
        }
    }
    public void Recycle(GameObject obj)
    {
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(obj);
    }
}
