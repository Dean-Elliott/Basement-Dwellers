using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public enum ElevatorStates
    {
        Resting = 0,
        GoingUp = 1,
        GoingDown = 2
    }
    ElevatorStates elevatorState;

    public GameObject elevatorFloor;

    public float speed;

    public GameObject[] waypoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (elevatorState)
        {
            case ElevatorStates.Resting:
                //Elevator is resting and awaiting input
                break;

            case ElevatorStates.GoingUp:
                GoUp();
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorState = ElevatorStates.GoingUp;
        }
    }

    private void GoUp()
    {
        if (elevatorFloor.transform.position.y < waypoints[1].transform.position.y)
        {
            elevatorFloor.transform.position += elevatorFloor.transform.up * speed * Time.deltaTime;
        }
    }
}
