using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTranslator : MonoBehaviour
{
    public string key;
    public void Start()
    {
        var tmpT = GetComponent<TMP_Text>();
        var T = GetComponent<Text>();

        if (tmpT)
        {
            tmpT.text = LangsList.GetWord(key);
        }
        if (T)
        {
            T.text = LangsList.GetWord(key);
        }
    }
}
