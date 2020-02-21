using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemy;

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
        elapsingTimeBetweenSpawns = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        currentActiveEnemies = TestEnemyController.enemiesInScene;

        if (currentActiveEnemies < maximumActiveEnemies)
        {
            elapsingTimeBetweenSpawns -= Time.deltaTime;
        }

        if (elapsingTimeBetweenSpawns <= 0.0f)
        {
            SpawnEnemy();
            elapsingTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = waypoints[Random.Range(0, waypoints.Length)].transform.position;
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
