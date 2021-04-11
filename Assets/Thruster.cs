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
    public ParticleSystem fog;
    private void Awake()
    {
        fire.SetActive(true);
        fire.GetComponent<ParticleSystem>().collision.SetPlane(0, GameObject.FindGameObjectWithTag("ParticlesCollider").transform);
    }
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

            if (pPart.jointConnectors.Count == 0)
            {
                GetComponent<SpriteRenderer>().sprite = normalThruster;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = hidedTruster;
                run = false;
            }
        }
        if (rb != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - transform.up, -transform.up, 10f);
            if (hit.transform != null && run)
            {
                fog.emissionRate = 50;
                fog.transform.position = hit.point;
                fog.collision.SetPlane(0, GameObject.FindGameObjectWithTag("ParticlesCollider").transform);
            }
            else
            {
                fog.emissionRate = 0;
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
        fire.GetComponent<ParticleSystem>().emissionRate = run ? 100 : 0;
        fire.transform.GetChild(0).gameObject.SetActive(run);
    }

}
