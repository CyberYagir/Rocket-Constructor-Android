using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSky : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Vector3 passiveScroll;
    public Sun sun;
    public float del = 1;
    private void Start()
    {
        sun = FindObjectOfType<Sun>();
    }

    private void Update()
    {
        if (UIManager.manager.simRocket == null)
        {
            sprite.material.SetVector("_ScrollSpeed", passiveScroll);
        }
        else
        {
            var vel = UIManager.manager.simRocket.GetComponent<Rigidbody2D>().velocity;
            passiveScroll = new Vector3(0.3f + (vel.x/ del), vel.y/100);
            sprite.material.SetVector("_ScrollSpeed", Camera.main.transform.position/del);
        }
        var n = sun.light.intensity + 0.1f;
        sprite.transform.localScale = Vector3.one * (Camera.main.transform.position.y > 1000 ?  1 + (2f * ((Camera.main.transform.position.y - 1000f) / 5000f)) : 1f);
        sprite.color = new Color(n, n, n, Camera.main.transform.position.y > 1000 ? 1f - ((Camera.main.transform.position.y - 1000f) / 5000f) : 1f);
    }
}
