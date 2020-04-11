using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class Wave
{
    public int enemiesPerWave;
    public GameObject enemy;
}

public class EnemySpawnerController : MonoBehaviour
{
    public Wave[] waves;
    [HideInInspector]
    public static int currentWave = 0;

    // Initialize all variables. Serialize where appropriate
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private int maximumActiveEnemies;

    private int currentActiveEnemies;

    [SerializeField]
    private float timeBetweenSpawns;

    [SerializeField]
    private Transform[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        TestEnemyController.enemiesKilled = 0;
        currentWave = 0;

        StartCoroutine("UpdateWave");
    }

    IEnumerator UpdateWave()
    {
        while (true)
        {
            // Get the the number of enemies in the scene
            currentActiveEnemies = TestEnemyController.enemiesInScene;

            // If there are no enemies in the scene, spawn a new wave
            if (currentActiveEnemies == 0)
            {
                SpawnWave(waves[currentWave]);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SpawnWave(Wave waveToSpawn)
    {
        // Spawn an enemy at a randomly selection waypoint
        for (int i = 0; i < waveToSpawn.enemiesPerWave; i++)
        {
            Vector3 spawnPosition = waypoints[Random.Range(0, waypoints.Length)].transform.position;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }

        currentWave++;
    }

}
