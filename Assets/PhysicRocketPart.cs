using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class JointConnector{
    public Rigidbody2D obj;
    public HingeJoint2D hingeJoint;
}

public class PhysicRocketPart : MonoBehaviour
{
    public GameObject parent;
    public List<JointConnector> jointConnectors = new List<JointConnector>();
    public float velDelta;
    float oldVel;
    private void FixedUpdate()
    {
        velDelta = oldVel - GetComponent<Rigidbody2D>().velocity.y;
        oldVel = GetComponent<Rigidbody2D>().velocity.y;
        if (velDelta < -3f)
        {
            Instantiate(Manager.explode.gameObject, transform.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, -1)) * 50f, ForceMode2D.Impulse);
            if (parent != null)
            {
                var parentjoint = parent.GetComponent<PhysicRocketPart>().jointConnectors.Find(x => x.obj == GetComponent<Rigidbody2D>());
                parentjoint.obj.AddRelativeForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, -1)) * 50f, ForceMode2D.Impulse);

                Destroy(parentjoint.hingeJoint);
                transform.parent = null;
            }
            for (int i = 0; i < jointConnectors.Count; i++)
            {
                if (jointConnectors[i].obj != null)
                    jointConnectors[i].obj.AddRelativeForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, -1)) * 50f, ForceMode2D.Impulse);
                Destroy(jointConnectors[i].hingeJoint);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public void ConnectAllChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(0).GetComponent<Rigidbody2D>() != null)
            {
                var joint = transform.gameObject.AddComponent<HingeJoint2D>();
                joint.useLimits = true;
                joint.limits = new JointAngleLimits2D() { min = 0, max = 0 };
                joint.connectedBody = transform.GetChild(0).GetComponent<Rigidbody2D>();
                transform.GetChild(0).GetComponent<PhysicRocketPart>().parent = gameObject;
                var conn = new JointConnector() { obj = transform.GetChild(0).GetComponent<Rigidbody2D>(), hingeJoint = joint };
                jointConnectors.Add(conn);
                transform.GetChild(0).parent = null;
            }
            else
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }
}
