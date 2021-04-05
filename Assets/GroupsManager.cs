using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupsManager : MonoBehaviour
{
    public List<Group> groups = new List<Group>();
    public Transform holder, item;
    public GameObject canvas, endCanvas;
    public bool select;
    public Group group;

    public Transform miniHolder, miniItem;

    public void FixedUpdate()
    {
        miniHolder.parent.parent.gameObject.SetActive(UIManager.simulate);
    }

    public void EndEdit()
    {
        for (int i = 0; i < group.parts.Count; i++)
        {
            group.parts[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        TouchManager.touchManager.enabled = true;
        select = false;
        canvas.SetActive(true);
        endCanvas.SetActive(false);
        UpdateList();
    }

    public void Active()
    {
        for (int i = 0; i < group.parts.Count; i++)
        {
            var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == group.parts[i].partFullName);
            if (n != null)
            {
                if (n.gameObject.GetComponent<Thruster>())
                {
                    n.gameObject.GetComponent<Thruster>().run = !n.gameObject.GetComponent<Thruster>().run;
                }
                if (n.gameObject.GetComponent<Cargo>())
                {
                    n.gameObject.GetComponent<Cargo>().Spawn();
                }
            }
        }
    }
    public void Detach()
    {
        if (group != null)
        {
            for (int i = 0; i < group.parts.Count; i++)
            {
                var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == group.parts[i].partFullName);
                if (n != null)
                {
                    n.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        for (int i = 0; i < group.parts.Count; i++)
        {
            var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == group.parts[i].partFullName);
            if (n != null)
            {
                n.GetComponent<PhysicRocketPart>().Detach();
            }
        }
        group.detach = true;
        UpdateList();
    }

    public void SelectParts()
    {
        if (!UIManager.simulate)
        {
            group.parts = new List<Part>();
            UIManager.manager.gameObject.SetActive(false);
            TouchManager.touchManager.enabled = false;
            select = true;
            endCanvas.SetActive(true);
        }
    }

    private void Update()
    {
        if (select)
            if (Input.touchCount == 1)
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    var ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector3.forward);
                    if (ray.collider != null)
                    {
                        if (ray.transform.tag != "NotDrag" && ray.transform.tag != "Ground")
                        {
                            if (group.parts.Contains(ray.transform.GetComponent<Part>()))
                            {
                                group.parts.Remove(ray.transform.GetComponent<Part>());
                                ray.transform.GetComponent<SpriteRenderer>().color = Color.white;
                            }
                            else
                            {
                                group.parts.Add(ray.transform.GetComponent<Part>());
                                ray.transform.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 55, 255);
                            }
                        }
                    }
                }
    }
    public void AddGroup()
    {
        groups.Add(new Group());
        UpdateList();
    }


    public void UpdateList()
    {
        foreach (Transform item in holder)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in miniHolder)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < groups.Count; i++)
        {
            if (!UIManager.simulate)
            {
                groups[i].detach = false;
            }
            if (!groups[i].detach)
            {
                var it = Instantiate(item.gameObject, holder).GetComponent<GroupItem>();
                it.text.text = "Group: " + i;
                it.group = groups[i];
                it.DrawItems();
                it.gameObject.SetActive(true);


                var itm = Instantiate(miniItem.gameObject, miniHolder).GetComponent<GroupMiniItem>();
                itm.name = "Group: " + i;
                itm.text.text = "Group: " + i;
                itm.group = groups[i];
                itm.gameObject.SetActive(true);
            }
        }
    }
}
