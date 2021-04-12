using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.volume = 1 - (Camera.main.transform.position.y / 5000f);
    }
    private void Update()
    {
        audioSource.volume = 1 - (Camera.main.transform.position.y / 5000f);
    }
}
