using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Group
{
    public List<Part> parts = new List<Part>();
    public bool detach;
}
public class Rocket : MonoBehaviour
{
    public GameObject explode;
    public List<Transform> parts;
    public bool exploded;

    public static float left = 0, right = 0;
    public static float offcet;

    private void Update()
    {
        offcet = left + right;


        if (UIManager.manager.simRocket != null)
        {
            Camera.main.gameObject.transform.position = UIManager.manager.simRocket.transform.position + new Vector3(0, 0, -10);

            if (UIManager.manager.simRocket.transform.position.y < 5000f)
            {
                if (UIManager.manager.simRocket.GetComponent<Rigidbody2D>().velocity.y < -20f)
                {
                    for (int i = 0; i < parts.Count; i++)
                    {
                        if (parts[i] != null)
                        {
                            parts[i].GetComponent<Rigidbody2D>().AddForce(Vector3.up * 20f, ForceMode2D.Force);
                        }
                    }
                }
                else if (UIManager.manager.simRocket.transform.position.y > 20f)
                {
                    for (int i = 0; i < parts.Count; i++)
                    {
                        if (parts[i] != null)
                        {
                            parts[i].GetComponent<Rigidbody2D>().gravityScale = ((5000f - UIManager.manager.simRocket.transform.position.y) / 5000f);
                            parts[i].GetComponent<Rigidbody2D>().drag = ((5000f - UIManager.manager.simRocket.transform.position.y) / 5000f) * 3f;
                            parts[i].GetComponent<Rigidbody2D>().angularDrag = ((5000f - UIManager.manager.simRocket.transform.position.y) / 5000f) * 3f;

                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    if (parts[i] != null)
                    {
                        parts[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                        parts[i].GetComponent<Rigidbody2D>().drag = 0;
                        parts[i].GetComponent<Rigidbody2D>().angularDrag = 0;

                    }
                }

            }
        }
    }

}
