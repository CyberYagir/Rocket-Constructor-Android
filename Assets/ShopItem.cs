using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerDownHandler
{
    public GameObject prefab;
    public void OnPointerDown(PointerEventData eventData)
    {
        Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.touches[0].position), Quaternion.identity).GetComponent<PartBuilder>().Unconnect();
        Shop.canvas.SetActive(false);
        PlayerPrefs.SetInt("Shop", 1);
    }
}
