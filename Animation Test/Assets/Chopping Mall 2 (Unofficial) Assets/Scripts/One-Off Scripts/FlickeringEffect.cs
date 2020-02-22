using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aura2API;

public class FlickeringEffect : MonoBehaviour
{
    // Initialize components
    private AudioSource audioSourceComponent;
    [SerializeField]
    private AuraVolume auraVolumeComponent;

    // Set up and initialize state enumerator
    public enum FlickerStates
    {
        ColourA = 0,
        ColourB = 1
    }
    private FlickerStates flickerState;

    // Initialize all variables
    [SerializeField]
    private AudioClip buzz;    

    [SerializeField]
    private float minimumColourADuration;
    [SerializeField]
    private float maximumColourADuration;
    private float elapsingColourADuration;

    [SerializeField]
    private float minimumColourBDuration;
    [SerializeField]
    private float maximumColourBDuration;
    private float elapsingColourBDuration;

    [SerializeField]
    private Color colourA;
    [SerializeField]
    private Color colourB;

    private bool isPlayingBuzzSound;

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
        // Decrement the appropriate timer by elapsing time
        if (flickerState == FlickerStates.ColourA)
        {
            elapsingColourADuration -= Time.deltaTime;
        }
        else if (flickerState == FlickerStates.ColourB)
        {
            elapsingColourBDuration -= Time.deltaTime;
        }

        // Change colour, change light state and reset elapsing colour duration when timer A reaches 0
        if (elapsingColourADuration <= 0.0f)
        {
            auraVolumeComponent.lightInjection.color = colourB;
            flickerState = FlickerStates.ColourB;
            elapsingColourADuration = Random.Range(minimumColourADuration, maximumColourADuration);
        }

        // Change colour, change light state, reset elapsing colour duration and play buzz sound when timer B reaches 0
        if (elapsingColourBDuration <= 0.0f)
        {
            if (isPlayingBuzzSound == true)
            {
                audioSourceComponent.PlayOneShot(buzz);
            }
            auraVolumeComponent.lightInjection.color = colourA;
            flickerState = FlickerStates.ColourA;
            elapsingColourBDuration = Random.Range(minimumColourBDuration, maximumColourBDuration);
        }
    }
}
