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
    private int currentAmmo;
    public int maxAmmo = 10;
    public float reloadTime = 4f;
    private bool isReloading = false;
    public Transform shotText;

    void Start ()
    {
        currentAmmo = maxAmmo;
    }

 
    void OnEnable()
    {
        currentAmmo = maxAmmo;
        shotText.GetComponent<Text>().text = currentAmmo.ToString();
        isReloading = false;
        
    }
    // Use this for initialization
    void Update()
    {
        if (isReloading)
            return;
       
        if (currentAmmo <= 0)
        {
           StartCoroutine(Reload());
            return;
        }
        //Instantiate Game Object
        if (Input.GetButtonDown("Fire1"))
        {
            gunShot.Play();
            muzzleFlash.Play();
            GameObject bullet = Instantiate(asset, spawnPoint.position, spawnPoint.rotation);
            currentAmmo--;
            shotText.GetComponent<Text>().text = currentAmmo.ToString();
        }

        IEnumerator Reload ()
        {
            isReloading = true;
            Debug.Log("Reloading");
            shotText.GetComponent<Text>().text = ("Reloading");

            yield return new WaitForSeconds(reloadTime);
            
            currentAmmo = maxAmmo;
            isReloading = false;
            shotText.GetComponent<Text>().text = currentAmmo.ToString();
        }
       
    }
 
}
