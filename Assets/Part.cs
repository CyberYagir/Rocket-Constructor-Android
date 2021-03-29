using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string partName;
    public string partFullName;
    public bool mode = false;
    public bool randomName = true;
    public void Start()
    {
        if (randomName)
        {
            transform.name = partName + ": " + Random.Range(0, 100000).ToString("000000");
            partFullName = transform.name;
        }
    }
}
