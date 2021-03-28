using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteEarth : MonoBehaviour
{
    public GameObject[] fon;
    public GameObject darkDown;
    
    private void Update()
    {
        Camera.main.backgroundColor = new Color(0.3f, 1f, 1f) * (transform.position.y < 40f ? 1 : ((10000f - transform.position.y)/10000f));
        if (transform.position.y < -5)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -5f, transform.position.z), 5f * Time.deltaTime);
        }
        var camPos = new Vector2(transform.position.x, 0);
        darkDown.transform.position = new Vector3(camPos.x, darkDown.transform.position.y, darkDown.transform.position.z);


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
