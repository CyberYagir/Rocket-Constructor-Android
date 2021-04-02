using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroupMiniItem : MonoBehaviour, IPointerDownHandler
{
    public Group group;
    public TMP_Text text;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponentInParent<MiniGroups>().group != null)
        {
            for (int i = 0; i < GetComponentInParent<MiniGroups>().group.parts.Count; i++)
            {
                var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == GetComponentInParent<MiniGroups>().group.parts[i].partFullName);
                if (n != null)
                {
                    n.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        GetComponentInParent<MiniGroups>().group = group;
        if (GetComponentInParent<MiniGroups>().button == gameObject)
        {

            GetComponentInParent<MiniGroups>().menu.active = !GetComponentInParent<MiniGroups>().menu.active;

        }
        else
        {
            GetComponentInParent<MiniGroups>().menu.active = true;
        }
        for (int i = 0; i < group.parts.Count; i++)
        {
            var n = FindObjectsOfType<Part>().ToList().Find(x => x.partFullName == group.parts[i].partFullName);
            if (n != null)
            {
                n.GetComponent<SpriteRenderer>().color = GetComponentInParent<MiniGroups>().menu.active ? Color.green : Color.white;
            }
        }
        GetComponentInParent<MiniGroups>().button = gameObject;
    }
}
