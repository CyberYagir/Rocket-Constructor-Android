using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{


    private void Start()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 0)
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Camera.main.transform.position.y < 3000)
        {
            if (GetComponent<AudioSource>().volume > 0.1f)
            {
                GetComponent<AudioSource>().volume -= 0.01f * Time.unscaledDeltaTime;
            }
        }
        else
        {
            if (GetComponent<AudioSource>().volume < 0.2f)
            {
                GetComponent<AudioSource>().volume += 0.01f * Time.unscaledDeltaTime;
            }
        }
    }
}
