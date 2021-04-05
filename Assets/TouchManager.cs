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
        if (selected != null && !UIManager.simulate)
        {
            selected.parent = null;
            var sel = selected.GetComponent<PartBuilder>();
            if (Input.touchCount >= 1)
            {
                selected.position = Vector2.Lerp((Vector2)selected.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position), magnetSpeed * Time.deltaTime);
                
                float mass = 0;
                float fuel = 0;
                foreach (var item in selected.GetComponentsInChildren<Part>())
                {
                    mass += item.mass;
                    if (item is FuelTank)
                    {
                        fuel += ((FuelTank)item).maxFuel;
                    }
                }
                UIManager.manager.infoText.transform.parent.gameObject.SetActive(true);
                UIManager.manager.infoText.text = "Selected: " + selected.name + "\nMass:" + mass + $"({selected.GetComponent<Part>().mass.ToString()}) t." + (fuel == 0 ? "" : "\nFuel: " + fuel);
            }
            else
            {
                if (sel != null)
                {
                    if (sel.connectPoints.Count != 0) {
                        sel.Connect();

                        if (sel.transform.parent == null && FindObjectOfType<TurretHandle>().mainRocket.gameObject != selected.gameObject)
                        {
                            if (FindObjectOfType<TurretHandle>().mainRocket.gameObject != selected.gameObject)
                            {
                                foreach (var item in selected.GetComponentsInParent<Part>())
                                {
                                    Player.money += item.cost;
                                }
                            }

                            Destroy(selected.gameObject);
                        }
                    }
                    else
                    {
                        if (FindObjectOfType<TurretHandle>().mainRocket.gameObject != selected.gameObject)
                        {
                            foreach (var item in selected.GetComponentsInParent<Part>())
                            {
                                Player.money += item.cost;
                            }

                            Destroy(selected.gameObject);
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

                UIManager.manager.infoText.transform.parent.gameObject.SetActive(false);
                selected = null;

            }
        }
       

        if (Input.touchCount == 1 && selected == null)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector3.forward);
                if (ray.collider != null)
                {
                    if (ray.transform.tag != "NotDrag" && ray.transform.tag != "Ground")
                    {
                        selected = ray.transform;
                    }
                }
            }
        }
        else if (Input.touchCount == 2 && selected == null)
        {
            camera.transform.Translate(-Input.GetTouch(0).deltaPosition * cameraSpeed * Time.deltaTime);
        }
    }
}
