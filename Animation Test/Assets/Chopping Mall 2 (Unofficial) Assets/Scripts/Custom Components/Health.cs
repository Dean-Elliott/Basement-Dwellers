using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float healthMax;
    public float damage;
    public bool Dead = false;
    public Animator animator;
    public ParticleSystem blood;
    private Projectile Bullet;
    private Player PM;
    private AudioSource sounds;
    [SerializeField]
    private AudioClip robotHurt;
    private void Start()
    {
        health = healthMax;
        PM = gameObject.GetComponent<Player>();
        sounds = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (this.gameObject.tag == "Target")
            {
                sounds.PlayOneShot(robotHurt);
                Bullet = collision.gameObject.GetComponent<Projectile>();
                health = health - Bullet.Damage;
                Destroy(collision.gameObject);
            }
            
        }

        
    }

    void CheckHealth()
    {
        if (health > healthMax)
        {
            health = healthMax;
        }

        if (health <= 0)
        {
            if(this.gameObject.tag == "Target")
            {
                Destroy(this.gameObject);
            }

            if(this.gameObject.tag == "Player")
            {
                Dead = true;
                animator.SetBool("IsDead", Dead);
                PM.CanMove = false;
                PM.CanLookAround = false;
            }
            
        }
    }
}
