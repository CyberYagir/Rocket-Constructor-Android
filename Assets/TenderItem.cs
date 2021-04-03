using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TenderItem : MonoBehaviour
{
    public Image image;
    public TMP_Text name_, target, mission, reward, workers;
    public Tender tender;


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
