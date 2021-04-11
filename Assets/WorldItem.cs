using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    private void Start()
    {
        if (GetComponentInChildren<TMP_Text>().text == "New")
        {
            GetComponentInChildren<TMP_Text>().gameObject.AddComponent<TextTranslator>().key = "New";
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void Click()
    {
        if (GetComponentInChildren<TMP_Text>().text != LangsList.GetWord("New"))
        {
            PlayerPrefs.SetString("WorldLoad", GetComponentInChildren<TMP_Text>().text);
        }
        else
        {
            PlayerPrefs.DeleteKey("WorldLoad");
        }
        Application.LoadLevel(1);
    }

    public void Delete(TMP_Text t)
    {
        print(FindObjectOfType<MainMenu>().filePath + @"\" + t.text + ".yWorld");
        File.Delete(FindObjectOfType<MainMenu>().filePath + @"\" + t.text + ".yWorld");
        FindObjectOfType<MainMenu>().UpdateWorlds();
    }
}
