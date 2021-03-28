using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandle : MonoBehaviour
{
    public Transform yHandle;
    public SpriteRenderer hand;
    public GameObject mainRocket;
    private void Update()
    {
        yHandle.gameObject.transform.position = new Vector3(yHandle.gameObject.transform.position.x, mainRocket.transform.position.y, yHandle.gameObject.transform.position.z);
        hand.size = new Vector2(Vector2.Distance(yHandle.transform.position, mainRocket.transform.position), 0.45f);
    }
}
