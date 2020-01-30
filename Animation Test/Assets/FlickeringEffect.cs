using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aura2API;

public class FlickeringEffect : MonoBehaviour
{
    public AuraVolume auraVolumeComponent;
    
    public float minimumInactiveDuration;
    public float maximumInactiveDuration;
    private float elapsingInactiveDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        elapsingInactiveDuration = Random.Range(minimumInactiveDuration, maximumInactiveDuration);
    }

    // Update is called once per frame
    void Update()
    {
        auraVolumeComponent.lightInjection.color = Color.blue;
    }
}
