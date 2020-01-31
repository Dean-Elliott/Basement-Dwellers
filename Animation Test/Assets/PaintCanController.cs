using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCanController : MonoBehaviour
{
    public AudioClip[] metalHitSounds;

    AudioSource audioSourceComponent;

    public float delayBeforeSoundActivation;

    private bool soundActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceComponent = gameObject.GetComponent<AudioSource>();
        StartCoroutine(DelaySoundActivation(delayBeforeSoundActivation));
    }

    IEnumerator DelaySoundActivation(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        soundActivated = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (soundActivated == true)
        {
            audioSourceComponent.PlayOneShot(metalHitSounds[Random.Range(0, metalHitSounds.Length)]);
        }
    }
}
