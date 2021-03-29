using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator shop, groups;
    public GameObject turret;
    public List<GameObject> allParts;
    public GameObject rocket;
    public GameObject simRocket;
    public GameObject shopB, startB, stopB;
    public static UIManager manager;
    public static bool simulate;
    public TMP_Text infoText;
    public GameObject controls;
    public void Start()
    {
        manager = this;
    }

    public void ShowShop()
    {
        shop.Play("Show");
    }

    public void HideShop()
    {
        shop.Play("Hide");
    }

    public void ShowGroups()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(groups.GetComponent<RectTransform>());
        groups.Play("Show");
    }

    public void HideGroups()
    {
        groups.Play("Hide");
    }
    private void Update()
    {
        if (simulate)
        {
            if (simRocket != null)
            {
                float mass = 0;
                var p =  FindObjectsOfType<Part>();
                for (int i = 0; i < p.Length; i++)
                {
                    mass += p[i].GetComponent<Rigidbody2D>().mass;
                }
                var rb = simRocket.GetComponent<Rigidbody2D>();
                infoText.text =
                    $"Altitude: {simRocket.transform.position.y.ToString("000 000")} m." + "\n"
                   + $"Velocity: {rb.velocity.x.ToString("000")} - {rb.velocity.y.ToString("000")} km." + "\n"
                   + $"Mass: {mass.ToString("0000.00")} t.";
            }
        }
    }

    public void StopSimulation()
    {
        simulate = false;
        for (int i = 0; i < allParts.Count; i++)
        {
            Destroy(allParts[i].gameObject);
        }
        simRocket = null;
        Camera.main.transform.position = new Vector3(-0.87f, 0, -10f);
        rocket.SetActive(true);

        shopB.SetActive(true);
        startB.SetActive(true);
        stopB.SetActive(false);
        turret.SetActive(true);
        controls.SetActive(false);
        infoText.transform.parent.gameObject.SetActive(false);
    }
    public void StartSimulation()
    {
        rocket = FindObjectOfType<TurretHandle>().mainRocket.gameObject;
        if (rocket == null) return;
        infoText.transform.parent.gameObject.SetActive(true);

        simulate = true;

        controls.SetActive(true);
        TouchManager.selected = null;
        shopB.SetActive(false);
        startB.SetActive(false);
        stopB.SetActive(true);
        
        allParts = new List<GameObject>();
        rocket.SetActive(false);
        simRocket = Instantiate(rocket.gameObject, rocket.transform.position, rocket.transform.rotation);
        simRocket.SetActive(true);
        turret.gameObject.SetActive(false);

        var p = FindObjectOfType<Rocket>();
        p.parts = new List<Transform>(); 
        foreach (var item in simRocket.GetComponentsInChildren<PartBuilder>())
        {
            item.GetComponent<Part>().randomName = false;
            var rb = item.gameObject.AddComponent<Rigidbody2D>();
            rb.mass = item.GetComponent<Part>().mass;
            rb.drag = 1;
            rb.angularDrag = 1;
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
