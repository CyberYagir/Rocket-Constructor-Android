using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thruster : MonoBehaviour
{
    public float force;
    public float sharedForce;
    public GameObject fire;
    public SpriteRenderer icon;
    public bool mode = false;
    private void Start()
    {
        sharedForce = force;
    }
    private void Update()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * sharedForce * Time.deltaTime, ForceMode2D.Force);
            fire.SetActive(true);
        }
    }

    public void ModeChange()
    {
        mode = !mode;
        if (!mode)
        {
            icon.color = new Color(0.5f, 0.5f, 0.5f);
            sharedForce = force;
        }
        else
        {
            icon.color = Color.red;
            sharedForce = force * 1.2f;
        }
    }
}
