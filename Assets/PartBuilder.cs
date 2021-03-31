using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PartBuilder : MonoBehaviour
{
    public Transform[] points;
    public List<Transform> currPins = new List<Transform>(), connectPins = new List<Transform>();



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
    public void Connect()
    {
        for (int i = 0; i < currPins.Count; i++)
        {
            currPins[i].gameObject.SetActive(false);
            connectPins[i].gameObject.SetActive(false);
            connectPins[i].parent.GetComponent<PartBuilder>().connectPins.Add(currPins[i]);
            connectPins[i].parent.GetComponent<PartBuilder>().currPins.Add(connectPins[i]);
        }
        
        transform.position = connectPins[0].position - currPins[0].localPosition;
        transform.parent = connectPins[0].parent.transform;
        for (int i = 0; i < points.Length; i++)
        {
            points[i].tag = "Pin";
        }
    }
    public void Unconnect()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].tag = "Untagged";
        }
        transform.parent = null;
        var allchilds = GetComponentsInChildren<PartBuilder>();

        for (int i = 0; i < currPins.Count; i++)
        {
            if (connectPins[i].parent.parent != transform)
            {
                connectPins[i].parent.GetComponent<PartBuilder>().connectPins.Remove(currPins[i]);
                connectPins[i].parent.GetComponent<PartBuilder>().currPins.Remove(connectPins[i]);
                currPins[i].gameObject.SetActive(true);
                connectPins[i].gameObject.SetActive(true);
                currPins.Remove(currPins[i]);
                connectPins.Remove(connectPins[i]);
            }
        }
        for (int j = 0; j < allchilds.Length; j++)
        {
            for (int i = 0; i < allchilds[j].connectPins.Count; i++)
            {
                if (!allchilds.Contains(allchilds[j].connectPins[i].parent.GetComponent<PartBuilder>()))
                {
                    allchilds[j].connectPins[i].parent.GetComponent<PartBuilder>().connectPins.Remove(allchilds[j].currPins[i]);
                    allchilds[j].connectPins[i].parent.GetComponent<PartBuilder>().currPins.Remove(allchilds[j].connectPins[i]);
                    
                    allchilds[j].currPins[i].gameObject.SetActive(true);
                    allchilds[j].connectPins[i].gameObject.SetActive(true);

                    allchilds[j].currPins.Remove(allchilds[j].currPins[i]);
                    allchilds[j].connectPins.Remove(allchilds[j].connectPins[i]);
                }
            }
        }

        currPins = new List<Transform>();
        connectPins = new List<Transform>();
    }
    private void Update()
    {
        if (Input.touchCount == 1 && TouchManager.selected == transform)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Unconnect();
            }
            currPins = new List<Transform>();
            connectPins = new List<Transform>();
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
                if (id != -1 && min < 0.55f)
                {
                    if (pins[id].GetComponent<PinType>().Check(points[j].GetComponent<PinType>()))
                    {
                        currPins.Add(points[j].transform);
                        connectPins.Add(pins[id].transform);
                    }
                }
            }

            print(currPins.Count);
        }



    }
}
