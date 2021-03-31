using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Thruster : Part
{
    public float force;
    public float sharedForce;
    public bool run;
    public float fueleat;
    public Rigidbody2D rb;
    public GameObject fire;
    public SpriteRenderer icon;
    public PhysicRocketPart pPart;
    public Sprite hidedTruster, normalThruster;

    
    private void Update()
    {
        if (rb == null)
        {
            sharedForce = force;
            rb = GetComponent<Rigidbody2D>();
        }
        if (pPart == null)
        {
            pPart = GetComponent<PhysicRocketPart>();
        }

        if (!pPart)
        {
            foreach (Transform item in transform)
            {
                if (item.GetComponent<PartBuilder>())
                {
                    GetComponent<SpriteRenderer>().sprite = hidedTruster; return;
                }
            }
            GetComponent<SpriteRenderer>().sprite = normalThruster;
        }
        else
        {

            if (pPart.jointConnectors.Count <= 1)
            {
                GetComponent<SpriteRenderer>().sprite = normalThruster;
            }
            else
            {
                if (pPart.jointConnectors[0].hingeJoint == null)
                {
                    GetComponent<SpriteRenderer>().sprite = normalThruster;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = hidedTruster;
                }
            }
        }

        if (rb != null && run)
        {
            var prnt = transform;
            bool can = false;
            while (can == false)
            {
                prnt = prnt.gameObject.GetComponent<PhysicRocketPart>().parent;
                if (prnt != null)
                {
                    if (prnt.GetComponent<FuelTank>() != null)
                    {
                        if (prnt.GetComponent<FuelTank>().fuel > fueleat)
                        {
                            prnt.GetComponent<FuelTank>().fuel -= fueleat * Time.deltaTime;
                            break;
                        }
                    }
                    else
                    {
                        run = false;
                        return;
                    }
                }
                else
                {
                    run = false;
                    return;
                }
            }

            rb.AddRelativeForce((Vector3.up + new Vector3(Rocket.offcet,0)) * sharedForce * Time.deltaTime, ForceMode2D.Force);
        }
        fire.SetActive(run);
        
    }

    public void ModeChange()
    {
        mode = !mode;
        if (!mode)
        {
            icon.color = new Color(0.5f, 0.5f, 0.5f);
            sharedForce = force;
        }
        else
        {
            icon.color = Color.red;
            sharedForce = force * (force * 0.001f);
        }
    }
}
