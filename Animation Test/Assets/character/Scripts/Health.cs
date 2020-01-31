using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float healthMax;
    public float damage;
    public ParticleSystem blood;

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blood.Play();
            health -= damage;
        }
    }

    void CheckHealth()
    {
        if (health > healthMax)
        {
            health = healthMax;
        }

        if (health < 0)
        {
            health = 0;
        }
    }
}
