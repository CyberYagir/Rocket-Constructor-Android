using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandle : MonoBehaviour
{
    public Transform yHandle, endHandle;
    public SpriteRenderer hand;
    public Part mainRocket;
    private void Update()
    {
        if (mainRocket != null)
        {
            if (mainRocket.transform.position.y > endHandle.transform.position.y)
            {
                mainRocket.transform.position = Vector3.Lerp(mainRocket.transform.position, new Vector3(mainRocket.transform.position.x, endHandle.transform.position.y - 2f, mainRocket.transform.position.z), 10 * Time.deltaTime);
                TouchManager.selected = null;
            }
            if (mainRocket.transform.position.y < -5f)
            {
                mainRocket.transform.position = Vector3.Lerp(mainRocket.transform.position, new Vector3(mainRocket.transform.position.x, -4f, mainRocket.transform.position.z), 10 * Time.deltaTime);
                TouchManager.selected = null;
            }
            yHandle.gameObject.transform.position = new Vector3(yHandle.gameObject.transform.position.x, mainRocket.transform.position.y, yHandle.gameObject.transform.position.z);
            hand.size = new Vector2((mainRocket.transform.position.x > hand.transform.position.x ? -1 : 1) * Vector2.Distance(yHandle.transform.position, mainRocket.transform.position), 0.45f);
        }
        else
        {
            mainRocket = FindObjectOfType<Part>();
        }
    }
}
