using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LangEditor : EditorWindow
{
    public string findWord = "";
    public Vector2 scroll = new Vector2(0,0);
    public bool setKeyRusWord;
    public int tab;
    [MenuItem("YagirLib/Langs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LangEditor));
    }


    private void OnGUI()
    {
        if (LangsList.langs == null)
        {
            LangsList.langs = FindObjectOfType<LangsList>();
            if (LangsList.langs == null)
            {
                GUILayout.Label("Cant find LangsList.cs script on scene", EditorStyles.boldLabel, GUILayout.Width(position.width));
                if (GUILayout.Button("Init"))
                {
                    GameObject gm = new GameObject();
                    gm.name = "Languages";
                    gm.AddComponent<LangsList>();
                }
                return;
            }
        }
        if (LangsList.langs.translates == null)
        {
            GUILayout.Label("Set Translates.assets", EditorStyles.boldLabel, GUILayout.Width(position.width));

            LangsList.langs.translates = EditorGUILayout.ObjectField("Asset: ", LangsList.langs.translates, typeof(LangListObjects), true) as LangListObjects;
            return;
        }

        EditorWindow.GetWindow(typeof(LangEditor)).minSize = new Vector2(500, 300);

        tab = GUILayout.Toolbar(tab, new string[] { "Words", "Languages" });

        if (tab == 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Search: ", EditorStyles.boldLabel, GUILayout.Width(100));
            findWord = EditorGUILayout.TextArea(findWord, GUILayout.Width(position.width - 120));
            GUILayout.EndHorizontal();
            setKeyRusWord = GUILayout.Toggle(setKeyRusWord, "Key = Rus", GUILayout.Width(200));
            GUILayout.Space(20);
            var words = LangsList.langs.translates.words;

            if (GUILayout.Button("Add Word"))
            {
                LangsList.langs.translates.words.Insert(0, new WordKey());
                findWord = "";
            }

            GUILayout.Space(20);
            GUILayout.Label("Words: ", EditorStyles.boldLabel);
            scroll = GUILayout.BeginScrollView(
                scroll, GUILayout.Width(position.width), GUILayout.Height(position.height - 180));

            int id = 0;
            foreach (var item in words)
            {
                if (findWord.Trim() != "")
                {
                    bool isDraw = false;
                    for (int i = 0; i < item.phrases.Count; i++)
                    {
                        if (item.phrases[i].phrase.ToLower().Contains(findWord.ToLower()))
                        {
                            isDraw = true;
                        }
                    }
                    if (!isDraw) continue;
                }

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical(GUILayout.Width(150));
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.key, EditorStyles.boldLabel);
                id++;
                if (item.hide)
                {
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button("Hide"))
                    {
                        item.hide = !item.hide;
                        OnGUI();
                        return;
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        LangsList.langs.translates.words.Remove(item);
                        OnGUI();
                        return;
                    }
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                    continue;
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Hide"))
                {
                    item.hide = !item.hide;
                    OnGUI();
                    return;
                }
                if (GUILayout.Button("Remove"))
                {
                    LangsList.langs.translates.words.Remove(item);
                    OnGUI();
                    return;
                }
                item.key = EditorGUILayout.TextArea(item.key);
                
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                for (int i = 0; i < item.phrases.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(item.phrases[i].langName + ": ", GUILayout.Width(100));
                    var opt = new GUILayoutOption[] { GUILayout.Width(position.width - 300), GUILayout.ExpandWidth(true) };

                    item.phrases[i].phrase = EditorGUILayout.TextArea(item.phrases[i].phrase, opt);
                    if (setKeyRusWord)
                    {
                        item.key = item.phrases[0].phrase;
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.Space(20);
                id++;
            }
            GUILayout.EndScrollView();
            if (GUILayout.Button("Add Word"))
            {
                LangsList.langs.translates.words.Insert(0, new WordKey() { key = "Key", hide = true});
                findWord = "";
            }
        }
        else
        {
            GUILayout.Label("Languages List: ", EditorStyles.boldLabel, GUILayout.Width(100));

            var langs = LangsList.langs.translates.languages;
            var words = LangsList.langs.translates.words;
            for (int i = 0; i < langs.Count; i++)
            {
                GUILayout.BeginHorizontal();
                var old = langs[i];
                langs[i] = EditorGUILayout.TextArea(langs[i], GUILayout.Width((position.width/2)));
                if (old != langs[i])
                {
                    for (int j = 0; j < words.Count; j++)
                    {
                        var ln = words[j].phrases.Find(x => x.langName == old);
                        if (ln != null)
                            ln.langName = langs[i];
                    }
                }
                if (GUILayout.Button("Remove", GUILayout.Width((position.width / 2) - 10)))
                {
                    for (int j = 0; j < words.Count; j++)
                    {
                        words[j].phrases.RemoveAll(x => x.langName == langs[i]);
                    }
                    langs.Remove(langs[i]);
                    OnGUI();
                    return;
                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Language", GUILayout.Width(position.width)))
            {
                langs.Add("Language: " + langs.Count);
                for (int j = 0; j < words.Count; j++)
                {
                    words[j].phrases.Add(new LangPhrase() { langName = langs[langs.Count - 1], phrase = "" });
                }

                OnGUI();
                return;
            }
        }
    }
}
