using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpawnObject : MonoBehaviour {

    [System.Serializable]
    public class BulletPool
    {
        public string tag;
        public GameObject asset;
        public int maxAmmo;
        
    }
    //Initializing Variables
    public List<BulletPool> bulletPools;
    public Dictionary<string, Queue<GameObject>> bulletDictionary;
    public Transform spawnPoint;
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;
    public int currentAmmo;
    public float reloadTime = 4f;
    [SerializeField]
    private bool isReloading = false;
    public Transform shotText;

 
    void OnEnable()
    {
        bulletDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (BulletPool bullet in bulletPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < bullet.maxAmmo; i++)
            {
                GameObject bullets = Instantiate(bullet.asset);
                bullets.SetActive(false);
                objectPool.Enqueue(bullets);
            }

            bulletDictionary.Add(bullet.tag, objectPool);
        }
        //Set ammo to max
        currentAmmo = bulletPools[0].maxAmmo;
        shotText.GetComponent<Text>().text = currentAmmo.ToString();
        isReloading = false;
        
    }
    // Use this for initialization
    void Update()
    {
        Debug.Log(bulletPools[0].maxAmmo);
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
            BulletPooler(bulletPools[0].tag, spawnPoint.position, spawnPoint.rotation);
            currentAmmo--;//Redue current ammo
            shotText.GetComponent<Text>().text = currentAmmo.ToString();
        }

        IEnumerator Reload ()
        {
            isReloading = true; //set reloading to true
            Debug.Log("Reloading");
            shotText.GetComponent<Text>().text = ("Reloading");

            yield return new WaitForSeconds(reloadTime);
            
            currentAmmo = bulletPools[0].maxAmmo;
            isReloading = false;
            shotText.GetComponent<Text>().text = currentAmmo.ToString();
        }
       
    }

    public GameObject BulletPooler(string tag, Vector3 position, Quaternion rotation)
    {
     
        GameObject spawnBullet = bulletDictionary[tag].Dequeue();
        spawnBullet.SetActive(true);
        spawnBullet.transform.position = position;
        spawnBullet.transform.rotation = rotation;

        bulletDictionary[tag].Enqueue(spawnBullet);

        return spawnBullet;
    }
 
}
