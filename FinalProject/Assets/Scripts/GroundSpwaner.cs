using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpwaner : MonoBehaviour
{
    public GameObject UnitPrefeb;
    public int numberOfWaves;
    public int enemiesPerWave;
    public int secondBetweenWave;
    public int secondsStartDelay;
    private int _currentWave = 0;
    public List<GameObject> _activeGrounds = new List<GameObject>();
    private Action OnDeath;
    private Action OnRecycle;
    private GameObject _go;


    private void Awake()
    {
        if (UnitPrefeb == null)
        {
            Debug.LogError("UnitSpawner disabled: Unit Prefab is NULL");
            gameObject.SetActive(false);
            return;
        }
    }

    public void Init(System.Action onEnemnyKilled = null)
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

    private void SpawnWave(int waveNumber)
    {
        for (int i = 0; i < enemiesPerWave; ++i)
        {
            //GameObject enemy = GameObject.Instantiate(UnitPrefeb, transform.position, transform.rotation);
            GameObject ground = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(UnitPrefeb.name);
            ground.transform.position = transform.position;
            ground.SetActive(true);
            OnRecycle = () => Recycle(ground);
            ground.GetComponent<Ground>().Initialize(OnRecycle);
            _activeGrounds.Add(ground);
        }
    }

    public void Recycle(GameObject obj)
    {
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(obj);
    }

    void OnDestroy()
    {
        OnDeath -= OnKilled;
    }

    public void OnKilled()
    {
        Destroy(gameObject);
    }
}
