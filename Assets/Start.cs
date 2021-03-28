using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public GameObject[] fon;
    
    private void FixedUpdate()
    {
        if (transform.position.y > 40)
        {
            for (int i = 0; i < fon.Length; i++)
            {
                fon[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1) *  (1f - ((10040f - transform.position.y) / 10040f));
            }
        }
        else
        {
            for (int i = 0; i < fon.Length; i++)
            {
                fon[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void Update()
    {
        var camPos = new Vector2(transform.position.x, 0);
        if (Vector2.Distance(camPos, new Vector2(fon[1].transform.position.x, 0)) <= 5f)
        {
            fon[0].transform.position = fon[1].transform.position - new Vector3(12f, 0, 0);
            fon[2].transform.position = fon[1].transform.position + new Vector3(12f, 0, 0);
        }
        if (Vector2.Distance(camPos, new Vector2(fon[0].transform.position.x, 0)) <= 5f)
        {
            fon[1].transform.position = fon[0].transform.position;
            fon[0].transform.position = fon[1].transform.position - new Vector3(12f, 0, 0);
            fon[2].transform.position = fon[1].transform.position + new Vector3(12f, 0, 0);
        }
        if (Vector2.Distance(camPos, new Vector2(fon[2].transform.position.x, 0)) <= 5f)
        {
            fon[1].transform.position = fon[2].transform.position;
            fon[0].transform.position = fon[1].transform.position - new Vector3(12f, 0, 0);
            fon[2].transform.position = fon[1].transform.position + new Vector3(12f, 0, 0);
        }
        if ((camPos.x < fon[0].transform.position.x || camPos.x > fon[2].transform.position.x) && Vector2.Distance(camPos, new Vector2(fon[0].transform.position.x, 0)) > 12)
        {
            int posPlus = (int)camPos.x;
            int posMin = (int)camPos.x;
            while (posPlus % 12 != 0 && posMin % 12 != 0)
            {
                posMin--;
                posPlus++;
            }
            fon[1].transform.position = new Vector2(posPlus % 12 == 0 ? posPlus : posMin, fon[1].transform.position.y);
            fon[0].transform.position = fon[1].transform.position - new Vector3(12f, 0, 0);
            fon[2].transform.position = fon[1].transform.position + new Vector3(12f, 0, 0);
        }
    }
}
