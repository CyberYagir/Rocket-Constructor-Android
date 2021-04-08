using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class ConnectPoints
{
    public Transform connectPin;
    public Transform objectPin;
    public bool toDel;
}
public class PartBuilder : MonoBehaviour
{
    public Transform[] points;
    public List<ConnectPoints> connectPoints = new List<ConnectPoints>();



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
        for (int i = 0; i < connectPoints.Count; i++)
        {
            connectPoints[i].connectPin.gameObject.SetActive(false);
            connectPoints[i].objectPin.gameObject.SetActive(false);
            if (connectPoints[i].connectPin.parent.GetComponent<PartBuilder>().connectPoints.Find(x => x.objectPin == connectPoints[i].connectPin && x.connectPin == connectPoints[i].objectPin) == null)
                connectPoints[i].connectPin.parent.GetComponent<PartBuilder>().connectPoints.Insert(0, new ConnectPoints() { objectPin = connectPoints[i].connectPin, connectPin = connectPoints[i].objectPin });
        }


        transform.position = connectPoints[0].connectPin.position - connectPoints[0].objectPin.localPosition;
        transform.parent = connectPoints[0].connectPin.parent.transform;
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
        if (transform.parent != null)
        {
            var parent = transform.parent.GetComponent<PartBuilder>();
            var connect = connectPoints.FindAll(x => x.connectPin.parent == parent.transform);
            for (int i = 0; i < connect.Count; i++)
            {
                connect[i].connectPin.gameObject.SetActive(true);
                connect[i].objectPin.gameObject.SetActive(true);
                connectPoints.Remove(connect[i]);
            }
            parent.connectPoints.RemoveAll(x => x.connectPin.parent == transform);
            transform.parent = null;


            var childs = GetComponentsInChildren<PartBuilder>().ToList();
            for (int i = 0; i < connectPoints.Count; i++)
            {
                if (!childs.Contains(connectPoints[i].connectPin.parent.GetComponent<PartBuilder>()))
                {
                    connectPoints[i].objectPin.gameObject.SetActive(true);
                    connectPoints[i].connectPin.gameObject.SetActive(true);
                    connectPoints[i].connectPin.parent.GetComponent<PartBuilder>().connectPoints.RemoveAll(x => x.connectPin.parent == transform);
                    connectPoints[i].toDel = true;
                }
            }
            connectPoints.RemoveAll(x => x.toDel);
        }
    }
    private void Update()
    {
        if (Input.touchCount == 1 && TouchManager.selected == transform)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Unconnect();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                FindAndConnect();
            }
        }
    }

    public void FindAndConnect()
    {
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
            if (id != -1 && min < 0.3f)
            {
                var f = connectPoints.Find(x => x.connectPin == pins[id].transform && x.objectPin == points[j].transform);

                if (pins[id].GetComponent<PinType>().Check(points[j].GetComponent<PinType>()))
                {
                    if (f == null)
                    {
                        connectPoints.Insert(0, new ConnectPoints() { connectPin = pins[id].transform, objectPin = points[j].transform });
                    }
                }
                else
                {
                    if (f != null)
                    {
                        connectPoints.Remove(f);
                    }
                }
            }
            else
            {
                var f = connectPoints.Find(x => x.connectPin == pins[id].transform && x.objectPin == points[j].transform);
                if (f != null)
                {
                    connectPoints.Remove(f);
                }
            }
        }
    }

    public void FindConnections(List<SaveLoad.Connections> connects)
    {
        Unconnect();
        var parts = FindObjectsOfType<Part>().ToList();
        for (int i = 0; i < connects.Count; i++)
        {
            var connected = parts.ToList().Find(x => x.partCode == connects[i].connectedObjectCode);
            print(connected.name);
        }
        Connect();
    }
}
