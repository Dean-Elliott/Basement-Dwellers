using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject Enemy;
}
public class SpawnManager : MonoBehaviour
{
    public Wave[] Waves; // class to hold information per wave
    public Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;
    public int _totalEnemiesInCurrentWave;
    public int _enemiesInWaveLeft;
    public int _spawnedEnemies;
    public int _currentWave;
    public int _totalWaves;
    public float nextSpawnTime = 5f;


    void Start()
    {
        _currentWave = -1; // avoid off by 1
        _totalWaves = Waves.Length - 1; // adjust, because we're using 0 index
        StartNextWave();
    }


    void StartNextWave()
    {
        _currentWave++;
        // win
        if (_currentWave > _totalWaves)
        {
            return;
        }
        _totalEnemiesInCurrentWave = Waves[_currentWave].EnemiesPerWave;
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }
    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        GameObject enemy = Waves[_currentWave].Enemy;
        while (_spawnedEnemies < _totalEnemiesInCurrentWave)
        {
            _spawnedEnemies++;
            _enemiesInWaveLeft++;
            int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(TimeBetweenEnemies);
        }
        yield return null;
    }

    // called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;


        // We start the next wave once we have spawned and defeated them all
        if (_enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {
           StartNextWave();
        }
    }

    public void Update()
    {
        if (_enemiesInWaveLeft <= 0)
        {
           

        }

        if (nextSpawnTime <= 0)
        {
            _enemiesInWaveLeft = 0;
            _spawnedEnemies = _totalEnemiesInCurrentWave;
            StartNextWave();      
        }

    
    }
}

