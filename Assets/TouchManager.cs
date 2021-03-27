using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static Transform selected;
    public float magnetSpeed;
    void Update()
    {
        if (selected != null)
        {
            selected.parent = null;
            var sel = selected.GetComponent<PartBuilder>();
            if (Input.touchCount >= 1)
            {
                selected.position = Vector2.Lerp((Vector2)selected.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position), magnetSpeed * Time.deltaTime);

                if (sel != null)
                {
                    for (int i = 0; i < sel.pinned.Count; i++)
                    {
                        sel.pinned[i].gameObject.SetActive(true);
                    }
                    for (int i = 0; i < sel.connectPoint.Count; i++)
                    {
                        sel.connectPoint[i].gameObject.SetActive(true);
                    }
                    sel.pinned = new List<Transform>();
                    sel.connectPoint = new List<Transform>();
                }
            }
            else
            {
                if (sel != null)
                {
                    if (sel.pin != null)
                    {
                        sel.transform.position = sel.pin.transform.position - sel.point.localPosition;
                        sel.pinned = new List<Transform>();
                        sel.connectPoint = new List<Transform>();
                        selected.parent = sel.pin.GetComponentInParent<PartBuilder>().transform;
                        var n = GameObject.FindGameObjectsWithTag("Pin");
                        for (int j = 0; j < sel.points.Length; j++)
                        {
                            for (int i = 0; i < n.Length; i++)
                            {
                                if (sel.points[j] != n[i].transform)
                                {
                                    if (Vector2.Distance(sel.points[j].position, n[i].transform.position) <= 0.1f)
                                    {
                                        sel.connectPoint.Add(sel.points[j].transform);
                                        sel.pinned.Add(n[i].transform);
                                        sel.points[j].gameObject.SetActive(false);
                                        n[i].gameObject.SetActive(false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (selected.transform.tag != "MainPart")
                            Destroy(selected.gameObject);
                    }
                    if (selected != null)
                    {
                        sel.setTag("Pin", true);
                    }
                    if (PlayerPrefs.GetInt("Shop", 0) == 1)
                    {
                        Shop.canvas.SetActive(true);
                        PlayerPrefs.DeleteKey("Shop");
                    }
                }
                selected = null;

            }
        }
        if (Input.touchCount >= 1 && selected == null)
        {
            var ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector3.forward);
            if (ray.collider != null && ray.transform.tag != "NotDrag")
            {
                selected = ray.transform;
            }
        }
    }
}
