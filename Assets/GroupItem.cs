using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroupItem : MonoBehaviour
{
    public Transform subholder;
    public Transform subitem;
    public TMP_Text text;
    public GameObject detachB, activeB, addB, removeB;
    public Group group;


    public void SelectParts()
    {
        var n = FindObjectOfType<GroupsManager>();
        n.select = true;
        n.group = group;
        n.SelectParts();
    }
    public void Active()
    {
        var n = FindObjectOfType<GroupsManager>();
        n.group = group;
        n.Active();
    }
    public void Detach()
    {
        var n = FindObjectOfType<GroupsManager>();
        n.group = group;
        n.Detach();
    }
    public void Remove()
    {
        var n = FindObjectOfType<GroupsManager>();
        n.groups.Remove(group);
        n.UpdateList();
    }
    private void Update()
    {
        detachB.active = UIManager.simulate;
        activeB.active = UIManager.simulate;
        addB.active = !UIManager.simulate;
        removeB.active = !UIManager.simulate;
    }
    public void DrawItems()
    {
        foreach (Transform item in subholder)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < group.parts.Count; i++)
        {
            if (group.parts[i] != null)
            {
                var it = Instantiate(subitem.gameObject, subholder);
                it.GetComponentInChildren<Image>().sprite = group.parts[i].GetComponent<SpriteRenderer>().sprite;
                var t = it.transform.GetChild(1).GetComponent<TMP_Text>();
                t.text = group.parts[i].partName;
                it.SetActive(true);
            }
            else
            {
                group.parts.RemoveAt(i);
                DrawItems();
                return;
            }
        }
    }
}
