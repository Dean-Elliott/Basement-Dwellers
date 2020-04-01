using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/*
[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject Enemy;
}
*/
public class SpawnManager : MonoBehaviour
{
    private static int _spawnedEnemies;

    public Wave[] Waves; // class to hold information per wave
    public Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;
    private int _totalEnemiesInCurrentWave;
    private int _enemiesInWaveLeft;
    private int _currentWave;
    private int _totalWaves;
    public float nextSpawnTime = 5f;

    void Start()
    {
        // Avoid off by 1
        _currentWave = -1;

        // Adjust, because we're using 0 index
        _totalWaves = Waves.Length - 1;
        StartNextWave();
    }


    void StartNextWave()
    {
        _currentWave++;
        // Win
        if (_currentWave > _totalWaves)
        {
            return;
        }
        _totalEnemiesInCurrentWave = Waves[_currentWave].enemiesPerWave;
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        GameObject enemy = Waves[_currentWave].enemy;
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

    /*
    // Called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;

        // We start the next wave once we have spawned and defeated them all
        if (_enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
    }
    */

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

