using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerDownHandler
{
    public GameObject prefab;
    public int cost;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.money >= cost)
        {
            var obj = Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.touches[0].position), Quaternion.identity);
            obj.GetComponent<PartBuilder>().Unconnect();
            Player.money -= cost;
            obj.GetComponent<Part>().cost = cost;
            Shop.canvas.SetActive(false);
            PlayerPrefs.SetInt("Shop", 1);
        }
    }
}
