﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Company
{
    public string name;
    public int peoples;
    public Sprite icon;
}
[System.Serializable]
public class Tender {
    public Company company;
    public int money;
    public GameObject prefab;
    public Planet planet;
    public enum Type {Fly, Deliver };
    public Type type;
    public int height;
    [TextArea]
    public string text;
    public bool ended;
}
public class Tenders : MonoBehaviour
{
    public static Tender currentTender;
    public Planet[] planets;
    public Company[] companies;
    public GameObject[] sattelites;
    public List<Tender> tenders;
    [Space]
    public Transform holder, item;
    public Animator UIEndWorndow; 

    void Start(){
        for (int i = 0; i < planets.Length; i++)
        {
            tenders.Add(new Tender() { company = companies[Random.Range(0, companies.Length)], money = ((int)planets[i].minY * 10) + (int)(1000000 * Random.value), prefab = sattelites[Random.Range(0, sattelites.Length)], planet = planets[i], type = Tender.Type.Deliver });
        }

        for (int i = 0; i < Random.Range(5,10); i++)
        {

            var t = (Tender.Type)Random.Range(0, 2);
            if (t == Tender.Type.Deliver)
            {
                var pl = planets[Random.Range(0, planets.Length)];
                tenders.Add(new Tender() { company = companies[Random.Range(0, companies.Length)], money = ((int)pl.minY * 10) + (int)(1000000f * Random.value), prefab = sattelites[Random.Range(0, sattelites.Length)], planet = pl, type = Tender.Type.Deliver });
            }
            else
            {
                var h = Random.Range(6000, 500000);
                tenders.Add(new Tender() { company = companies[Random.Range(0, companies.Length)], money = (int)((h * 10) * Random.value), height = h, type = Tender.Type.Fly });
            }
        }
        UpdateList();
    }
    private void Update()
    {
        if (currentTender != null)
        {
            if (currentTender.type == Tender.Type.Fly)
            {
                if (Camera.main.transform.position.y >= currentTender.height)
                {
                    currentTender.ended = true;
                }
            }
            if (currentTender.ended) {
                UIEndWorndow.Play("Show");
            }

        }
    }

    public void EndTender()
    {
        currentTender.ended = false;
        Player.money += currentTender.money;
        tenders.Remove(currentTender);
        currentTender = null;
        UIEndWorndow.Play("Hide");
        UIManager.manager.StopSimulation();
    }

    public void UpdateList(){
        print("UpdateList");
        foreach (Transform item in holder)
        {
            Destroy(item.gameObject);   
        }

        for (int i = 0; i < tenders.Count; i++)
        {
            if (!UIManager.simulate)
            {
                var it = Instantiate(item, holder).GetComponent<TenderItem>();
                it.tender = tenders[i];
                it.gameObject.SetActive(true);
            }
            else
            {
                if (currentTender != null)
                {
                    if (currentTender == tenders[i])
                    {
                        var it = Instantiate(item, holder).GetComponent<TenderItem>();
                        it.tender = tenders[i];
                        it.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    

}
