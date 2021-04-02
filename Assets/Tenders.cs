using System.Collections;
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
    [TextArea]
    public string text;
}



public class Tenders : MonoBehaviour
{
    public Planet[] planets;
    public Company[] companies;
    public GameObject[] sattelites;

    public List<Tender> tenders;
}
