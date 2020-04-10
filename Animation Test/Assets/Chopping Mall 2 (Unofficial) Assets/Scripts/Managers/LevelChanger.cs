using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public GameObject levelLoading;

    public void OnTriggerEnter(Collider other)
    {
        //when player enter the game object call level loader and load the next level
        if (other.gameObject.tag == "Player")
        {
            levelLoading.GetComponent<LevelLoader>().LoadNextLevel();    
        }
    }
}
