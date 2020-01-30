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
    public ElevatorStates elevatorState;

    public GameObject elevatorFloor;

    public float speed;

    public GameObject[] waypoints;

    public int currentWaypoint = 0;

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

            case ElevatorStates.GoingDown:
                GoDown();
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && elevatorState == ElevatorStates.Resting)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && currentWaypoint != waypoints.Length - 1)
            {
                elevatorState = ElevatorStates.GoingUp;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentWaypoint != 0)
            {
                elevatorState = ElevatorStates.GoingDown;
            }
        }
    }

    private void GoUp()
    {
        if (elevatorFloor.transform.position.y < waypoints[currentWaypoint + 1].transform.position.y)
        {
            elevatorFloor.transform.position += elevatorFloor.transform.up * speed * Time.deltaTime;
        }
        else
        {
            currentWaypoint++;
            elevatorState = ElevatorStates.Resting;
        }

        /*
        if (elevatorFloor.transform.position.y < waypoints[1].transform.position.y)
        {
            elevatorFloor.transform.position += elevatorFloor.transform.up * speed * Time.deltaTime;
        }
        */
    }

    private void GoDown()
    {
        if (elevatorFloor.transform.position.y > waypoints[currentWaypoint - 1].transform.position.y)
        {
            elevatorFloor.transform.position -= elevatorFloor.transform.up * speed * Time.deltaTime;
        }
        else
        {
            currentWaypoint--;
            elevatorState = ElevatorStates.Resting;
        }
    }
}
