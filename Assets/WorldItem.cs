using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public void Click()
    {
        if (GetComponentInChildren<TMP_Text>().text != "New")
        {
            PlayerPrefs.SetString("WorldLoad", GetComponentInChildren<TMP_Text>().text);
        }
        else
        {
            PlayerPrefs.DeleteKey("WorldLoad");
        }
        Application.LoadLevel(1);
    }
}
