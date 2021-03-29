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
        if (rb != null && run)
        {
            var parent = gameObject;


            bool can = false;
            while (can == false)
            {
                parent = parent.GetComponent<PhysicRocketPart>().parent;
                if (parent != null)
                {
                    print(parent.name);
                    if (parent.GetComponent<FuelTank>() != null)
                    {
                        if (parent.GetComponent<FuelTank>().fuel > fueleat)
                        {
                            parent.GetComponent<FuelTank>().fuel -= fueleat * Time.deltaTime;
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

            rb.AddRelativeForce(Vector3.up * sharedForce * Time.deltaTime, ForceMode2D.Force);
        }
        fire.SetActive(run);
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

            if (pPart.jointConnectors.Count == 0)
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
