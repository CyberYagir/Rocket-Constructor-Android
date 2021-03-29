using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroupItem : MonoBehaviour
{
    public Transform subholder;
    public Transform subitem;
    public TMP_Text text;
    public GameObject detachB, modeB, activeB, addB;
    public Group group;


    public void SelectParts()
    {
        var n = FindObjectOfType<GroupsManager>();
        n.select = true;
        n.group = group;
        n.SelectParts();
    }
    public void Active()
    {
        for (int i = 0; i < group.parts.Count; i++)
        {
            var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == group.parts[i].partFullName);
            if (n != null)
            {
                if (n.gameObject.GetComponent<Thruster>())
                {
                    n.gameObject.GetComponent<Thruster>().run = !n.gameObject.GetComponent<Thruster>().run;
                }
            }
        }
    }
    public void Detach()
    {
        for (int i = 0; i < group.parts.Count; i++)
        {
            var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == group.parts[i].partFullName);
            if (n != null)
            {
                n.GetComponent<PhysicRocketPart>().Detach();
            }
        }
    }
    private void Update()
    {
        detachB.active = UIManager.simulate;
        modeB.active = UIManager.simulate;
        activeB.active = UIManager.simulate;
        addB.active = !UIManager.simulate;
        
    }
    public void DrawItems()
    {
        foreach (Transform item in subholder)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < group.parts.Count; i++)
        {
            var it = Instantiate(subitem.gameObject, subholder);
            it.GetComponentInChildren<Image>().sprite = group.parts[i].GetComponent<SpriteRenderer>().sprite;
            var t = it.transform.GetChild(1).GetComponent<TMP_Text>();
            t.text = group.parts[i].partName;
            var thruster = group.parts[i].GetComponent<Thruster>();
            var fuel = group.parts[i].GetComponent<FuelTank>();
            it.transform.GetChild(2).GetComponent<TMP_Text>().text = "";
            it.transform.GetChild(2).GetComponent<TMP_Text>().text += (thruster != null ? "Mode: " + thruster.mode + "\n" : "");
            it.transform.GetChild(2).GetComponent<TMP_Text>().text += (fuel != null ? "Fuel: " + fuel.fuel + "/" + fuel.maxFuel + "\n" : "");

            it.SetActive(true);
        }
    }
}
