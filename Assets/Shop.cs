using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop shop;
    public static GameObject canvas;
    public GameObject cnv;
    private void Start()
    {
        shop = this;
        canvas = cnv;
    }
}
