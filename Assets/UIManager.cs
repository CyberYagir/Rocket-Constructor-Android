using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator shop;
    public void ShowShop()
    {
        shop.Play("Show");
    }

    public void HideShop()
    {
        shop.Play("Hide");
    }
}
