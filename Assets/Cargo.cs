using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cargo : MonoBehaviour 
{   
    public bool on;
    private void OnMouseDown()
    {
        on = true;
    }
    private void OnMouseExit()
    {
        on = false;

    }
    private void Update()
    {
        if (Input.touchCount == 1 && on)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                print("Up");
                on = false;
            }
        }
    }
}
