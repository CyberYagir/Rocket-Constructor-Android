using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string partName;


    public void Start()
    {
        partName += ": " + Random.Range(0, 100000).ToString("000000");
    }
}
