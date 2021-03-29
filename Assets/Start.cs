using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public GameObject[] fon;
    
    private void FixedUpdate()
    {
        if (transform.position.y > 40)
        {
            for (int i = 0; i < fon.Length; i++)
            {
                fon[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1) *  (1f - ((10040f - transform.position.y) / 10040f));
            }
        }
        else
        {
            for (int i = 0; i < fon.Length; i++)
            {
                fon[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void Update()
    {
        
    }
}
