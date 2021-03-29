using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RocketControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float sence;
    public bool left;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (left)
            Rocket.left = sence;
        else
            Rocket.right = sence;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (left)
            Rocket.left = 0;
        else
            Rocket.right = 0;
    }
}
