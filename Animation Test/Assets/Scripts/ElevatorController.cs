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
    private ElevatorStates elevatorState;

    public GameObject elevatorFloor;
    public Light elevatorLight;

    public float speed;

    public GameObject[] waypoints;

    public int currentWaypoint = 0;

    private bool elevatorActive = false;

    // Update is called once per frame
    void Update()
    {
        if (elevatorActive == true && elevatorState == ElevatorStates.Resting)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorActive = false;
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
            elevatorFloor.transform.position = waypoints[currentWaypoint + 1].transform.position;
            currentWaypoint++;
            elevatorState = ElevatorStates.Resting;
        }
    }

    private void GoDown()
    {
        if (elevatorFloor.transform.position.y > waypoints[currentWaypoint - 1].transform.position.y)
        {
            elevatorFloor.transform.position -= elevatorFloor.transform.up * speed * Time.deltaTime;
        }
        else
        {
            elevatorFloor.transform.position = waypoints[currentWaypoint - 1].transform.position;
            currentWaypoint--;
            elevatorState = ElevatorStates.Resting;
        }
    }
}
