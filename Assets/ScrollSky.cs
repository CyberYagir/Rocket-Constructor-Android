using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSky : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Vector3 passiveScroll;
    private void Update()
    {
        if (UIManager.manager.simRocket == null)
        {
            sprite.material.SetVector("_ScrollSpeed", passiveScroll);
        }
        else
        {
            var vel = UIManager.manager.simRocket.GetComponent<Rigidbody2D>().velocity;
            sprite.material.SetVector("_ScrollSpeed", new Vector4(passiveScroll.x + (vel.x / 100f), passiveScroll.y + (vel.y / 100f)));
        }
        sprite.color = new Color(FindObjectOfType<Sun>().light.intensity + 0.1f, 1, 1, 1f - Camera.main.transform.position.y / 5000f);
    }
}
