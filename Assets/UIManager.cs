﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator shop;
    public GameObject turret;
    public List<GameObject> allParts;
    GameObject rocket;
    public GameObject shopB, startB, stopB;
    public void ShowShop()
    {
        shop.Play("Show");
    }

    public void HideShop()
    {
        shop.Play("Hide");
    }

    public void StopSimulation()
    {
        for (int i = 0; i < allParts.Count; i++)
        {
            Destroy(allParts[i].gameObject);
        }
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        rocket.SetActive(true);

        shopB.SetActive(true);
        startB.SetActive(true);
        stopB.SetActive(false);
        turret.SetActive(true);
    }
    public void StartSimulation()
    {
        TouchManager.selected = null;
        shopB.SetActive(false);
        startB.SetActive(false);
        stopB.SetActive(true);
        
        allParts = new List<GameObject>();
        rocket = GameObject.FindGameObjectWithTag("MainPart");
        rocket.SetActive(false);
        GameObject simRocket = Instantiate(rocket.gameObject, rocket.transform.position, rocket.transform.rotation);
        simRocket.SetActive(true);
        turret.gameObject.SetActive(false);

        var p = simRocket.GetComponent<Rocket>();
        p.parts = new List<Transform>(); 
        foreach (var item in simRocket.GetComponentsInChildren<PartBuilder>())
        {
            item.gameObject.AddComponent<Rigidbody2D>();
            item.gameObject.AddComponent<PhysicRocketPart>();
            p.parts.Add(item.transform);
            item.transform.tag = "NotDrag";
            allParts.Add(item.gameObject);
            Destroy(item);
        }
        for (int i = 0; i < p.parts.Count; i++)
        {
            p.parts[i].GetComponent<PhysicRocketPart>().ConnectAllChilds();
        }
    }
}
