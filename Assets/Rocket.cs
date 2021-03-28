using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group
{
    public string name;
    public List<Group> groups = new List<Group>();
    public List<Thruster> thruster = new List<Thruster>();
    public List<FuelTank> fuelTanks = new List<FuelTank>();
}
public class Rocket : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject explode;
    public List<Transform> parts;
    public bool exploded;
    public List<Group> groups = new List<Group>();

    private void Update()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);

            if (transform.position.y < 5000f)
            {
                if (GetComponent<Rigidbody2D>().velocity.y < -20f)
                {
                    for (int i = 0; i < parts.Count; i++)
                    {
                        if (parts[i] != null)
                        {
                            parts[i].GetComponent<Rigidbody2D>().AddForce(Vector3.up * 20f, ForceMode2D.Force);
                        }
                    }
                }
                else if (transform.position.y > 20f)
                {
                    for (int i = 0; i < parts.Count; i++)
                    {
                        if (parts[i] != null)
                        {
                            parts[i].GetComponent<Rigidbody2D>().gravityScale = ((5000f - transform.position.y) / 5000f);
                        }
                    }
                }
            }
        }
    }

}
