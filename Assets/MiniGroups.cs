using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGroups : MonoBehaviour
{
    public GameObject menu;
    public Group group;
    public GameObject button;

    private void Update()
    {
        if (button != null)
        {
            menu.transform.position = new Vector3(menu.transform.position.x, button.transform.position.y);
        }
        else
        {
            menu.SetActive(false);
        }
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

}
