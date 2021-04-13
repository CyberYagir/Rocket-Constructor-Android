using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSounds : MonoBehaviour
{
    public AudioSource dayForest, nightForest;
    public Sun sun;


    private void Update()
    {
        dayForest.volume = (sun.light.intensity * 0.477f) * (1f -(Camera.main.transform.position.y/100f));
        nightForest.volume = ((1f - sun.light.intensity) * 0.066f) * (1f - (Camera.main.transform.position.y / 100f));
    }
}
