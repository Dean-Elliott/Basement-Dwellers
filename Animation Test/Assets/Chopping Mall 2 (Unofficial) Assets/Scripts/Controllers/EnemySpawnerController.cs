using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
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

    private float elapsingTimeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial spawn time
        elapsingTimeBetweenSpawns = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the the number of enemies in the scene
        currentActiveEnemies = TestEnemyController.enemiesInScene;

        // If the enemy count has not been maxed out, decrement the spawn timer
        if (currentActiveEnemies < maximumActiveEnemies)
        {
            elapsingTimeBetweenSpawns -= Time.deltaTime;
        }

        // If the elapsing time between spawns reaches 0, spawn an enemy and reset the timer
        if (elapsingTimeBetweenSpawns <= 0.0f)
        {
            SpawnEnemy();
            elapsingTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    public void SpawnEnemy()
    {
        // Spawn an enemy at a randomly selection waypoint
        Vector3 spawnPosition = waypoints[Random.Range(0, waypoints.Length)].transform.position;
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
