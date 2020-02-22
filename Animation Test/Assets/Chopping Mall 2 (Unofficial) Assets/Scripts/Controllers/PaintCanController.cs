using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCanController : MonoBehaviour
{
    // Initialize all referenced components
    private AudioSource audioSourceComponent;

    // Initialize variables
    public AudioClip[] metalHitSounds;
    public float delayBeforeSoundActivation;
    private bool soundActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceComponent = gameObject.GetComponent<AudioSource>();
        StartCoroutine(DelaySoundActivation(delayBeforeSoundActivation));
    }

    // Prevent individual sounds from playing excessively (once per frame, which is hard on the ears)
    IEnumerator DelaySoundActivation(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        soundActivated = true;
    }

    // On collision, play one, randomly selected hit sound effect
    private void OnCollisionEnter(Collision collision)
    {
        if (soundActivated == true)
        {
            audioSourceComponent.PlayOneShot(metalHitSounds[Random.Range(0, metalHitSounds.Length)]);
        }
    }
}
