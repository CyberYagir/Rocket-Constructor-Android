using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TenderItem : MonoBehaviour, IPointerDownHandler
{
    public Image image;
    public TMP_Text name_, target, mission, reward, workers;
    public Tender tender;
    public bool sel;
    public void OnPointerDown(PointerEventData eventData)
    {
        Tenders.currentTender = tender;
        GetComponent<Image>().color = new Color(0.2260591f, 0.3773585f, 0.2501912f, 0.8078431f);
        var m = FindObjectsOfType<TenderItem>().ToList().Find(x => x.sel);
        if (m != null)
        {
            m.sel = false;
            m.GetComponent<Image>().color = new Color(0.1792453f, 0.1665628f, 0.1665628f, 0.8078431f);
        }
        sel = true;

    }

    private void Start()
    {
        image.sprite = tender.company.icon;
        name_.text = tender.company.name;
        if (tender.type == Tender.Type.Deliver)
        {
            target.text = "Target: " + tender.planet.name + $" [{tender.planet.minY}-{tender.planet.maxY}]";
        }
        else
        {
            target.text = "Target: " + tender.height + " m.";
        }
        mission.text = "Mission: " + tender.type.ToString();
        reward.text = "Reward: " + tender.money;
        workers.text = "Workers\n " + tender.company.peoples;
    }
}
