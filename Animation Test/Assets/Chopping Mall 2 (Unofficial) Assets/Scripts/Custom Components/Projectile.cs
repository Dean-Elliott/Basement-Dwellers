using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float fireRate;
    public float LifeTime;
    public float Damage;
    public float CurrentTime;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
        Timer();

        if (Input.GetButtonDown("Fire1"))
        {
            gameObject.SetActive(true);
        }
        
    }

    void Timer()
    {

        if (gameObject.activeSelf == true)
        {
            CurrentTime = CurrentTime + 1 * Time.deltaTime;
        }
       

        if (CurrentTime >= LifeTime)
        {
            gameObject.SetActive(false);

            CurrentTime = 0;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Geo")
        {
            gameObject.SetActive(false);
        }
    }
}
