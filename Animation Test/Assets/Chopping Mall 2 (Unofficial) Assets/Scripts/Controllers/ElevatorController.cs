using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    // Initialize all referenced components
    private AudioSource audioSourceComponent;
    public Light elevatorLightComponent;

    // Set up and initialize state enumerator
    public enum ElevatorStates
    {
        Resting = 0,
        GoingUp = 1,
        GoingDown = 2
    }
    private ElevatorStates elevatorState;

    // Initialize all variables
    [SerializeField]
    private GameObject elevatorFloor;

    [SerializeField]
    private AudioClip dingSound;

    [SerializeField]
    private Color interactableColour;
    [SerializeField]
    private Color nonInteractableColour;

    [Range(0.1f, 100.0f)]
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject[] waypoints;

    private int currentWaypoint = 0;

    private bool elevatorActive = false;

    private void Start()
    {
        // Set immediately necessary referenced components
        audioSourceComponent = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elevatorState == ElevatorStates.Resting)
        {
            elevatorLightComponent.color = interactableColour;
        }
        else
        {
            elevatorLightComponent.color = nonInteractableColour;
        }

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

    // If the player is inside of the elevator's bounds, allow the elevator to be activated
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorActive = true;
        }
    }

    // If the player is outside of the elevator's bounds, do not allow the elevator to be activated
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorActive = false;
        }
    }

    // Move elevator up to the next floor
    private void GoUp()
    {
        if (elevatorFloor.transform.position.y < waypoints[currentWaypoint + 1].transform.position.y)
        {
            elevatorFloor.transform.position += elevatorFloor.transform.up * speed * Time.deltaTime;
        }
        else
        {
            audioSourceComponent.PlayOneShot(dingSound);
            elevatorFloor.transform.position = waypoints[currentWaypoint + 1].transform.position;
            currentWaypoint++;
            elevatorState = ElevatorStates.Resting;
        }
    }

    // Move elevator down to the previous floor
    private void GoDown()
    {
        if (elevatorFloor.transform.position.y > waypoints[currentWaypoint - 1].transform.position.y)
        {
            elevatorFloor.transform.position -= elevatorFloor.transform.up * speed * Time.deltaTime;
        }
        else
        {
            audioSourceComponent.PlayOneShot(dingSound);
            elevatorFloor.transform.position = waypoints[currentWaypoint - 1].transform.position;
            currentWaypoint--;
            elevatorState = ElevatorStates.Resting;
        }
    }
}
