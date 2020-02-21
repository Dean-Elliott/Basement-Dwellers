using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public bool hasKey = false;
    public GameObject key;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the player doesnt have the key set the game object to active (shows up)
        if (hasKey == false)
        {
            key.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //if the player collides with the key set haskey to true and set the keys active to false so it disapears
        if(other.gameObject.tag == "Key")
        {
            hasKey = true;
            key.SetActive(false);
        }
        //if the player goes infront of the lock area and they have the key open the door
        if (other.gameObject.tag == "Lock" && hasKey == true)
        {
            Debug.Log("its working but not");
            door.SetActive(false);
        }
    }
}
