using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string partName;
    public string partCode;
    public string partFullName;
    public float mass;
    public bool mode = false;
    public bool randomName = true;
    public float cost;
    public void Start()
    {
        if (randomName)
        {
            partCode = Random.Range(0, 100000).ToString("000000");
            transform.name = partName + ": " + partCode;
            partFullName = transform.name;
        }
    }
}
