using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroupMiniItem : MonoBehaviour, IPointerDownHandler
{
    public Group group;
    public TMP_Text text;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponentInParent<MiniGroups>().group = group;
        if (GetComponentInParent<MiniGroups>().button == gameObject)
        {
            GetComponentInParent<MiniGroups>().menu.active = !GetComponentInParent<MiniGroups>().menu.active;
        }
        else
        {
            GetComponentInParent<MiniGroups>().menu.active = true;
        }
        GetComponentInParent<MiniGroups>().button = gameObject;
    }
}
