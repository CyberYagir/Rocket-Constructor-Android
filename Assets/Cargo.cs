﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cargo : MonoBehaviour 
{   
    public bool drop = false;
    public void Spawn()
    {
        if (drop) return;
        if (UIManager.simulate)
        {
            if (Tenders.currentTender != null)
            {
                if (Tenders.currentTender.type == Tender.Type.Deliver)
                {
                    var sat = Instantiate(Tenders.currentTender.prefab, transform.position, transform.rotation);
                    if (Tenders.currentTender.planet.spriteRenderer.enabled)
                    {
                        sat.GetComponent<Sattelite>().moveToPlanet = true;
                        sat.GetComponent<Sattelite>().planet = Tenders.currentTender.planet.transform;
                        sat.GetComponent<Rigidbody2D>().isKinematic = true;
                        sat.GetComponent<Collider2D>().enabled = false;
                        sat.transform.parent = Camera.main.transform;
                    }
                    else
                    {
                        sat.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                    }
                    drop = true;
                }
            }
        }
    }
}
