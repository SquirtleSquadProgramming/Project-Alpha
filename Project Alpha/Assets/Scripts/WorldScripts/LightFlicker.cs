using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float dimAmmount = 0.3f;
    public float thresh = 0.2f;

    private float defaultIntensity;
    private Light lightComponent;
    private float seed;
    void Start()
    {
        seed = Random.Range(10, 100);
        lightComponent = this.GetComponent<Light>();
        defaultIntensity = lightComponent.intensity;
    }

    void Update() => lightComponent.intensity = flicker(thresh)*defaultIntensity;
    
    float flicker(float thresh)
    {
        float x = Time.time+seed;
        //https://www.desmos.com/calculator/2vuo2kemnx
        double d = Mathf.Sin(x) * Mathf.Sin((2 * x) + 1) + (0.2f * Mathf.Sin(27 * x)) + 0.5 * Mathf.Cos(0.1f * x);
        return d > thresh ? 1f : dimAmmount;
    }
}
