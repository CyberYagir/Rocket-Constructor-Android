using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLight : MonoBehaviour
{
    public Sun sun;
    public Light light;
    public void Start()
    {
        sun = FindObjectOfType<Sun>();
        light = GetComponent<Light>();
    }

    private void Update()
    {
        if (sun.light.intensity <= 0.2f)
        {
            light.intensity = 1;
        }
        else
        {
            light.intensity = 0;
        }
    }
}
