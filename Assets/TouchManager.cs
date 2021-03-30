using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static Transform selected;
    public static TouchManager touchManager;
    public float magnetSpeed, cameraSpeed;
    public Transform camera;

    private void Start()
    {
        touchManager = this;
    }

    void Update()
    {
        if (selected != null)
        {
            selected.parent = null;
            var sel = selected.GetComponent<PartBuilder>();
            if (Input.touchCount >= 1)
            {
                selected.position = Vector2.Lerp((Vector2)selected.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position), magnetSpeed * Time.deltaTime);

            }
            else
            {
                if (sel != null)
                {
                    if (sel.currPins.Count != 0) {
                        sel.Connect();
                    }
                    else
                    {
                        if (FindObjectsOfType<PartBuilder>().Length > 1 && FindObjectOfType<TurretHandle>().mainRocket.gameObject != selected.gameObject)
                        {
                            //Destroy(selected.gameObject);
                        }
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
        if (Input.touchCount == 1 && selected == null)
        {
            var ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector3.forward);
            if (ray.collider != null) {
                if (ray.transform.tag != "NotDrag" && ray.transform.tag != "Ground")
                {
                    selected = ray.transform;
                }
            }
        }
        else if (Input.touchCount == 2 && selected == null)
        {
            camera.transform.Translate(-Input.GetTouch(0).deltaPosition * cameraSpeed * Time.deltaTime);
        }
    }
}
