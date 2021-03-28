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
        simRocket.AddComponent<Rigidbody2D>();
    }
}
