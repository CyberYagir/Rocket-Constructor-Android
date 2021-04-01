using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float minY, maxY;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Camera.main.transform.position.y > minY - transform.localScale.y && Camera.main.transform.position.y < maxY + transform.localScale.y)
        {
            spriteRenderer.enabled = true;
            var percent = (Camera.main.transform.position.y - minY) / (maxY - minY);
            transform.position = new Vector3(Camera.main.transform.position.x, (Camera.main.transform.position.y + 25f) - (50f * percent));
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
