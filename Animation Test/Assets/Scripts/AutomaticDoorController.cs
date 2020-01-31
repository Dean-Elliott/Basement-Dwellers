using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorController : MonoBehaviour
{
    public int boink = 10000;
    public enum DoorStates
    {
        Resting = 0,
        Opening = 1,
        Closing = 2
    }
    public DoorStates doorStates;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public float speed;
    public float maximumSeperationDistance;
    public float minimumSeperationDistance;

    public int oink;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (doorStates)
        {
            case DoorStates.Resting:
                //Doors are resting and awaiting input
                break;

            case DoorStates.Opening:
                OpenDoors();
                break;

            case DoorStates.Closing:
                CloseDoors();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doorStates = DoorStates.Opening;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            doorStates = DoorStates.Closing;
        }
    }

    private void OpenDoors()
    {
        if (Vector3.Distance(leftDoor.transform.position, rightDoor.transform.position) < maximumSeperationDistance)
        {
            leftDoor.transform.position += leftDoor.transform.forward * speed * Time.deltaTime;
            rightDoor.transform.position -= rightDoor.transform.forward * speed * Time.deltaTime;
        }
        else
        {
            doorStates = DoorStates.Resting;
        }
    }

    private void CloseDoors()
    {
        if (Vector3.Distance(leftDoor.transform.position, rightDoor.transform.position) > minimumSeperationDistance)
        {
            leftDoor.transform.position -= leftDoor.transform.forward * speed * Time.deltaTime;
            rightDoor.transform.position += rightDoor.transform.forward * speed * Time.deltaTime;
        }
        else
        {
            doorStates = DoorStates.Resting;
        }
    }
}
