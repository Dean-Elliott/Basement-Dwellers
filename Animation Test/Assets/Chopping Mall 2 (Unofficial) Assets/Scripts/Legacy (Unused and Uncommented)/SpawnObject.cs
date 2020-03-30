using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpawnObject : MonoBehaviour {
    //Initializing Variables
    public GameObject asset;
    public Transform spawnPoint;
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;
    [SerializeField]
    private int currentAmmo;
    public int maxAmmo;
    public float reloadTime = 4f;
    [SerializeField]
    private bool isReloading = false;
    public Transform shotText;

 
    void OnEnable()
    {
        //Set ammo to max
        currentAmmo = maxAmmo;
        shotText.GetComponent<Text>().text = currentAmmo.ToString();
        isReloading = false;
        
    }
    // Use this for initialization
    void Update()
    {
        //Check if reloading
        if (isReloading)
            return;
        //If player has ammo, start reload coroutine
        if (currentAmmo <= 0)
        {
           StartCoroutine(Reload());
            return;
        }
        //Instantiate Game Object
        if (Input.GetButtonDown("Fire1"))
        {
            gunShot.Play(); //Play gun sound
            muzzleFlash.Play(); //Play particle effect
            GameObject bullet = Instantiate(asset, spawnPoint.position, spawnPoint.rotation);
            currentAmmo--;//Redue current ammo
            shotText.GetComponent<Text>().text = currentAmmo.ToString();
        }

        IEnumerator Reload ()
        {
            isReloading = true; //set reloading to true
            Debug.Log("Reloading");
            shotText.GetComponent<Text>().text = ("Reloading");

            yield return new WaitForSeconds(reloadTime);
            
            currentAmmo = maxAmmo;
            isReloading = false;
            shotText.GetComponent<Text>().text = currentAmmo.ToString();
        }
       
    }
 
}
