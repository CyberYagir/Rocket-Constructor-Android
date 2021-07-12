using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WordKey : Word
{
    public string key = "";
    public bool hide = true;
}

[System.Serializable]
public class Word
{
    public List<LangPhrase> phrases = new List<LangPhrase>();
    public Word()
    {

        try
        {
            for (int i = 0; i < LangsList.langs.translates.languages.Count; i++)
            {
                phrases.Add(new LangPhrase() { langName = LangsList.langs.translates.languages[i], phrase = "" });
            }
        }
        catch (System.Exception)
        {
        }
    }
    public Word(WordKey wordKey)
    {
        for (int i = 0; i < wordKey.phrases.Count; i++)
        {
            phrases.Add(wordKey.phrases[i]);
        }
    }
}

[System.Serializable]
public class LangPhrase
{
    public string langName;
    public string phrase;
}

[ExecuteInEditMode]
public class LangsList : MonoBehaviour
{
    public static LangsList langs;
    public static int currLang = 0;
    public LangListObjects translates;

    public static Dictionary<string, Word> dictionary = new Dictionary<string, Word>();


    private void Start()
    {
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Awake()
    {
        if (FindObjectsOfType<LangsList>().ToList().Find(x => x.gameObject != gameObject) != null)
        {
            Destroy(gameObject);
            return;
        }
        langs = this;
        dictionary = new Dictionary<string, Word>();
        if (translates != null)
        {
            for (int i = 0; i < translates.words.Count; i++)
            {
                dictionary.Add(translates.words[i].key, new Word(translates.words[i]));
            }
        }
        else
        {
            print("Set Translation Asset!");
        }
    }

    public static string GetWord(string key)
    {
        return dictionary[key].phrases[currLang].phrase;
    }
}