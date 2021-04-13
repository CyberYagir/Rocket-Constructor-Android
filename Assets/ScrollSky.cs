using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSky : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Vector3 passiveScroll;
    public Sun sun;
    public float del = 1;
    public float scale = 1;
    public float actionX, actionXSpeed;
    private void Start()
    {
        sun = FindObjectOfType<Sun>();
    }

    private void Update()
    {
        actionX += actionXSpeed * Time.deltaTime;
        passiveScroll = new Vector3(actionX, 0);
        sprite.material.SetVector("_ScrollSpeed", (Camera.main.transform.position / del) + passiveScroll);
        var n = sun.light.intensity + 0.1f;
        sprite.transform.localScale = Vector3.one * (Camera.main.transform.position.y > 1000 ?  1 + (2f * ((Camera.main.transform.position.y - 1000f) / 5000f)) : 1f) * scale;
        sprite.color = new Color(n, n, n, Camera.main.transform.position.y > 1000 ? 1f - ((Camera.main.transform.position.y - 1000f) / 5000f) : 1f);
    }
}
