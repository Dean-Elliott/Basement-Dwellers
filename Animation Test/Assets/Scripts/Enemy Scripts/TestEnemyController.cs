using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyController : MonoBehaviour
{
    private AudioSource audioSourceComponent;

    public static int enemiesInScene;

    private bool messageHasPlayed = false;
    public AudioClip killMessage;

    public enum EnemyStates
    {
        Travelling = 0,
        Attacking = 1,
        Stopped = 2
    }
    public EnemyStates enemyState;

    public List<GameObject> waypoints = new List<GameObject>();
    private int currentWaypoint = 0;
    private NavMeshAgent navMeshAgentComponent;
    private Health healthComponent;

    private Health enemyHealthComponent;

    public int attackDamage;
    public float attacksPerSecond;
    public float minimumAttackDistance;

    private float timeBetweenAttacks;
    private float elapsingTimeBetweenAttacks;

    private void Awake()
    {
        enemiesInScene++;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSourceComponent = gameObject.GetComponent<AudioSource>();

        navMeshAgentComponent = gameObject.GetComponent<NavMeshAgent>();
        healthComponent = gameObject.GetComponent<Health>();

        timeBetweenAttacks = 1f / attacksPerSecond;
        elapsingTimeBetweenAttacks = timeBetweenAttacks;

        navMeshAgentComponent.SetDestination(waypoints[currentWaypoint].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position));

        if (waypoints[currentWaypoint] != null)
        {
            if (enemyHealthComponent == null)
            {
                enemyHealthComponent = waypoints[currentWaypoint].GetComponent<Health>();
            }
            else if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= minimumAttackDistance)
            {
                enemyState = EnemyStates.Attacking;
            }
        }

        switch (enemyState)
        {
            case EnemyStates.Travelling:
                //Enemy is travelling to the next destination
                StartCoroutine(Travel());
                break;

            case EnemyStates.Attacking:
                AttackTarget();
                break;

            case EnemyStates.Stopped:
                navMeshAgentComponent.isStopped = true;
                break;
        }


    }

    IEnumerator Travel()
    {
        navMeshAgentComponent.SetDestination(waypoints[currentWaypoint].transform.position);
        yield return new WaitForSeconds(0.1f);
    }

    private void AttackTarget()
    {
        StopCoroutine(Travel());
        navMeshAgentComponent.isStopped = true;

        elapsingTimeBetweenAttacks -= Time.deltaTime;

        if (elapsingTimeBetweenAttacks <= 0.0f)
        {
            Debug.Log("I'm attacking!");
            enemyHealthComponent.health -= attackDamage;
            elapsingTimeBetweenAttacks = timeBetweenAttacks;
        }

        if (enemyHealthComponent.health <= 0)
        {
            if (messageHasPlayed == false)
            {
                audioSourceComponent.PlayOneShot(killMessage);
                messageHasPlayed = true;
            }

            if (waypoints.Count > 1)
            {
                waypoints.RemoveAt(currentWaypoint);
                GoToNextWaypoint();
                enemyState = EnemyStates.Travelling;
                messageHasPlayed = false;
            }
            else
            {
                enemyState = EnemyStates.Stopped;
            }

        }
        else if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) > minimumAttackDistance)
        {
            enemyState = EnemyStates.Travelling;
            navMeshAgentComponent.isStopped = false;
        }
    }

    private void GoToNextWaypoint()
    {
        navMeshAgentComponent.isStopped = false;

        if (currentWaypoint >= waypoints.Count - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint++;
        }

        navMeshAgentComponent.SetDestination(waypoints[currentWaypoint].transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            healthComponent.health--;
        }
    }

    public void OnHealthDepleted()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        enemiesInScene--;
    }
}
