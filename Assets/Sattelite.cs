using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sattelite : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < 5000f)
        {
            if (transform.position.y > 20f)
            {
                GetComponent<Rigidbody2D>().gravityScale = ((5000f - transform.position.y) / 5000f);
                GetComponent<Rigidbody2D>().drag = ((5000f - transform.position.y) / 5000f) * 3f;
                GetComponent<Rigidbody2D>().angularDrag = ((5000f - transform.position.y) / 5000f) * 3f;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().drag = 0;
            GetComponent<Rigidbody2D>().angularDrag = 0;
        }
    }
}
