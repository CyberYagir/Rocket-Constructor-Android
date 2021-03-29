using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : Part
{
    public float fuel, maxFuel;
    public bool init;
    public GameObject indic;


    private void Update()
    {
        if (!init)
        {
            if (GetComponent<Rigidbody2D>() != null)
            {
                GetComponent<Rigidbody2D>().mass = 2;
                GetComponent<Rigidbody2D>().drag = 3;
                GetComponent<Rigidbody2D>().angularDrag = 3;
                init = true;
            }
        }
        else
        {
            indic.transform.localScale = new Vector3((fuel / maxFuel), 1, 1);
            GetComponent<Rigidbody2D>().mass = ((fuel / maxFuel) * 1.5f) + 0.5f;
        }
    }
}
