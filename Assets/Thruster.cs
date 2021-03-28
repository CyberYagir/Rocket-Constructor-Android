using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public float force;
    public GameObject fire;
    private void Update()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * force * Time.deltaTime, ForceMode2D.Force);
            fire.SetActive(true);
        }
    }
}
