using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator menu;
    public string filePath;

    public Transform item, holder;
    public TMP_Dropdown dropdown;
    

    public void SetLaguage(){
        LangsList.currLang = dropdown.value;
        PlayerPrefs.SetInt("Lang", (int)dropdown.value);
        Application.LoadLevel(0);
    }

    private void Awake()
    {
        dropdown.value = PlayerPrefs.GetInt("Lang", 0);
        LangsList.currLang = dropdown.value;
        PlayerPrefs.SetInt("Lang", (int)dropdown.value);
    }
    private void Start()
    {
        menu.Play("Show");
        dropdown.onValueChanged.AddListener(delegate {
            SetLaguage();
        });
        filePath = Application.persistentDataPath;
    }
    public void Show(Animator animator)
    {
        animator.Play("Show");
    }
    public void Hide(Animator animator)
    {
        animator.Play("Hide");
    }
    public void UpdateWorlds()
    {
        foreach (Transform it in holder)
        {
            Destroy(it.gameObject);
        }

        var first = Instantiate(item.gameObject, holder);
        first.GetComponentInChildren<TMP_Text>().text = "New";
        first.SetActive(true);
        foreach (var it in Directory.GetFiles(filePath, "*.yWorld"))
        {
            var g = Instantiate(item.gameObject, holder);
            g.GetComponentInChildren<TMP_Text>().text = Path.GetFileNameWithoutExtension(it);
            g.SetActive(true);
        }
    }
}