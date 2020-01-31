using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aura2API;

public class FlickeringEffect : MonoBehaviour
{
    private AudioSource audioSourceComponent;

    public enum FlickerStates
    {
        ColourA = 0,
        ColourB = 1
    }
    private FlickerStates flickerState;

    public AudioClip buzz;

    public AuraVolume auraVolumeComponent;

    public float minimumColourADuration;
    public float maximumColourADuration;
    private float elapsingColourADuration;

    public float minimumColourBDuration;
    public float maximumColourBDuration;
    private float elapsingColourBDuration;

    public Color colourA;
    public Color colourB;

    public bool playBuzzSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceComponent = gameObject.GetComponent<AudioSource>();

        flickerState = FlickerStates.ColourA;
        auraVolumeComponent.lightInjection.color = colourA;

        elapsingColourADuration = Random.Range(minimumColourADuration, maximumColourADuration);
        elapsingColourBDuration = Random.Range(minimumColourBDuration, maximumColourBDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (flickerState == FlickerStates.ColourA)
        {
            elapsingColourADuration -= Time.deltaTime;
        }
        else if (flickerState == FlickerStates.ColourB)
        {
            elapsingColourBDuration -= Time.deltaTime;
        }

        if (elapsingColourADuration <= 0.0f)
        {
            auraVolumeComponent.lightInjection.color = colourB;
            flickerState = FlickerStates.ColourB;
            elapsingColourADuration = Random.Range(minimumColourADuration, maximumColourADuration);
        }

        if (elapsingColourBDuration <= 0.0f)
        {
            if (playBuzzSoundEffect == true)
            {
                audioSourceComponent.PlayOneShot(buzz);
            }
            auraVolumeComponent.lightInjection.color = colourA;
            flickerState = FlickerStates.ColourA;
            elapsingColourBDuration = Random.Range(minimumColourBDuration, maximumColourBDuration);
        }
    }
}
