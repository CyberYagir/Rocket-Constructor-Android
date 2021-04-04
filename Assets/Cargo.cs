using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cargo : MonoBehaviour 
{   
    public bool on;
    private void OnMouseDown()
    {
        print("down");
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
                if (UIManager.simulate)
                {
                    if (Tenders.currentTender != null)
                    {
                        if (Tenders.currentTender.type == Tender.Type.Deliver)
                        {
                            var sat = Instantiate(Tenders.currentTender.prefab, transform.position, transform.rotation);
                            sat.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                        }
                    }
                }
                on = false;
            }
        }
    }
}
