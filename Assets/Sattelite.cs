using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sattelite : MonoBehaviour
{
    public bool moveToPlanet;
    public Transform planet;

    
    private void Update()
    {
        if (!UIManager.simulate)
        {
            Destroy(gameObject);
        }

        if (!moveToPlanet)
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
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, planet.transform.position, 2 * Time.deltaTime);
            if (transform.localScale.x > 0)
                transform.localScale -= Vector3.one * Time.deltaTime;
            if (transform.localScale.x < 0)
            {
                Tenders.currentTender.ended = true;
                transform.localScale = Vector3.zero;
            }
        }
    }
}
