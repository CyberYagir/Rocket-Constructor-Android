using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator shop;
    public GameObject turret;
    public void ShowShop()
    {
        shop.Play("Show");
    }

    public void HideShop()
    {
        shop.Play("Hide");
    }


    public void StartSimulation()
    {
        var rocket = GameObject.FindGameObjectWithTag("MainPart");
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
            Destroy(item);
        }
        for (int i = 0; i < p.parts.Count; i++)
        {
            p.parts[i].GetComponent<PhysicRocketPart>().ConnectAllChilds();
        }

    }
}
