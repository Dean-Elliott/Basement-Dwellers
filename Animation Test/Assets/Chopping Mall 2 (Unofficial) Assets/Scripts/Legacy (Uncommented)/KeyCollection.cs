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
        if(hasKey == false)
        {
            key.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Key")
        {
            hasKey = true;
            key.SetActive(false);
        }

        if (other.gameObject.tag == "Lock" && hasKey == true)
        {
            Debug.Log("its working but not");
            door.SetActive(false);
        }
    }
}
