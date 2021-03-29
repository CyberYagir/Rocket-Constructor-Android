using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PartBuilder : MonoBehaviour
{
    public Transform[] points;
    public Transform pin, point;
    public List<Transform> pinned = new List<Transform>(), connectPoint = new List<Transform>();



    public void setTag(string tag, bool log = false)
    {
        if (points[0].tag != tag)
        {
            var builders = GetComponentsInChildren<PartBuilder>().ToList();
            builders.Add(this);
            for (int i = 0; i < builders.Count; i++)
            {
                for (int j = 0; j < builders[i].points.Length; j++)
                {
                    builders[i].points[j].tag = tag;
                }
            }
        }
    }
    private void Update()
    {
        if (Input.touchCount == 1 && TouchManager.selected == transform)
        {
            setTag("Untagged");
            var pins = GameObject.FindGameObjectsWithTag("Pin").ToList();
            for (int j = 0; j < points.Length; j++)
            {
                var min = 999999f;
                
                var id = -1;
                for (int i = 0; i < pins.Count; i++)
                {
                    var dist = Vector2.Distance(points[j].transform.position, pins[i].transform.position);
                    if (dist < min)
                    {
                        min = dist;
                        id = i;
                    }
                }
                if (id != -1 && min < 0.4f)
                {
                    if (pins[id].GetComponent<PinType>().Check(points[j].GetComponent<PinType>()))
                    {
                        pin = pins[id].transform;
                        point = points[j];
                        break;
                    }
                    else
                    {
                        pin = null;
                        point = null;
                    }
                }
                else
                {
                    pin = null;
                    point = null;
                }
            }
        }
        else
        {
            pin = null;
            point = null;
        }
    }
}
