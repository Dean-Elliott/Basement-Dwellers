using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {
    //Initializing Variables
    public GameObject asset;
    public Transform spawnPoint;
    public ParticleSystem muzzleFlash;

    // Use this for initialization
    void Update()
    {
        //Instantiate Game Object
        if (Input.GetButtonDown("Fire1") && PauseMenu.GameIsPaused == false)
        {
            muzzleFlash.Play();
            GameObject bullet = Instantiate(asset, spawnPoint.position, spawnPoint.rotation);
        }
       
    }
 
}
