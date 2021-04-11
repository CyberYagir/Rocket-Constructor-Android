using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float time;
    public float daySpeed;
    public Light light;
    private void Update()
    {
        if (time <= 100)
        {
            light.intensity = (time / 100f);
        }
        time += Time.deltaTime * daySpeed;
        if (time >= 200 && time <= 300)
        {
            light.intensity = 1-((time-200f)/100f);
        }
        if (time >= 400)
        {
            time = 0;
        }
    }
}
