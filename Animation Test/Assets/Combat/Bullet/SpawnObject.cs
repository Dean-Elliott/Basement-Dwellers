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
        if (Input.GetButton("Fire1"))
        {
            muzzleFlash.Play();
            Instantiate(asset, spawnPoint.position, Quaternion.identity);
        }
       
    }
 
}
