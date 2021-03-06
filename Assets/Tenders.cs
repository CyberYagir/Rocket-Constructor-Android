using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int conpanyID;
    [System.NonSerialized]
    public Company company;
    public int money;
    public int prefabID;
    [System.NonSerialized]
    public GameObject prefab;
    public int planetID;
    [System.NonSerialized]
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
    public TMP_Text endText;

    void Start(){
        for (int i = 0; i < planets.Length; i++)
        {
            tenders.Add(new Tender() { company = companies[Random.Range(0, companies.Length)], money = ((int)planets[i].minY * 10) + (int)(1000000 * Random.value), prefab = sattelites[Random.Range(0, sattelites.Length)], planet = planets[i], type = Tender.Type.Deliver });
        }

        for (int i = 0; i < Random.Range(5,10); i++)
        {
            AddTender();
        }
        for (int i = 0; i < Random.Range(2, 5); i++)
        {

            var t = (Tender.Type)Random.Range(0, 2);
            if (t == Tender.Type.Deliver)
            {
                var pl = planets[Random.Range(0, planets.Length)];
                tenders.Add(new Tender() { company = companies[Random.Range(0, companies.Length)], money = ((int)pl.minY * 10) + (int)(1000000f * Random.value), prefab = sattelites[Random.Range(0, sattelites.Length)], planet = pl, type = Tender.Type.Deliver });
            }
            else
            {
                var h = Random.Range(3000, 100000);
                tenders.Add(new Tender() { company = companies[Random.Range(0, companies.Length)], money = (int)((h * 10) * Random.value * 2), height = h, type = Tender.Type.Fly });
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
                endText.text = $"{currentTender.money}$";
                UIEndWorndow.Play("Show");
            }

        }
    }

    public void EndTender(bool restart)
    {
        currentTender.ended = false;
        Player.money += currentTender.money;
        tenders.Remove(currentTender);
        currentTender = null;
        UIEndWorndow.Play("Hide");
        if (restart)
            UIManager.manager.StopSimulation();
    }

    public void AddTender()
    {
        var t = (Tender.Type)Random.Range(0, 2);
        var compID = Random.Range(0, companies.Length);
        if (t == Tender.Type.Deliver)
        {
            var plID = Random.Range(0, planets.Length);
            var pl = planets[plID];
            var prefabID = Random.Range(0, sattelites.Length);
            tenders.Add(new Tender() { company = companies[compID], money = ((int)pl.minY * 10) + (int)(1000000f * Random.value), prefab = sattelites[prefabID], planet = pl, type = Tender.Type.Deliver, prefabID = prefabID, conpanyID = compID, planetID = plID });
        }
        else
        {
            var h = Random.Range(6000, 500000);
            tenders.Add(new Tender() { company = companies[compID], money = (int)((h * 10) * Random.value), height = h, type = Tender.Type.Fly, conpanyID = compID });
        }
    }

    public void UpdateList(){
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
