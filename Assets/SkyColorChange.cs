using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColorChange : MonoBehaviour
{
    public Color light;
    public Sun sun;
    public void Start()
    {
        light = GetComponent<SpriteRenderer>().color;
        sun = FindObjectOfType<Sun>();
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = (light * sun.light.intensity) + new Color(0, 0, 0, 1);
    }
}
