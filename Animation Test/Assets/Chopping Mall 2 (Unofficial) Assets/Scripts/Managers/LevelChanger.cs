using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public GameObject levelLoading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //when player enter the game object call level loader and load the next level
        if (other.gameObject.tag == "Player")
        {
            levelLoading.GetComponent<LevelLoader>().LoadNextLevel();    
        }
    }
}
