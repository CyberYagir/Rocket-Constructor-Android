using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class JointConnector{
    public Rigidbody2D obj;
    public FixedJoint2D hingeJoint;
}

public class PhysicRocketPart : MonoBehaviour
{
    public GameObject parent;
    public List<JointConnector> jointConnectors = new List<JointConnector>();
    public float velDelta;
    float oldVel;
    bool init;
    private void FixedUpdate()
    {
        if (!init)
        {
            for (int i = 0; i < jointConnectors.Count; i++)
            {
                if (jointConnectors[i].hingeJoint != null)
                {
                    jointConnectors[i].hingeJoint.autoConfigureConnectedAnchor = false;
                }
            }
            init = false;
        }
        velDelta = oldVel - GetComponent<Rigidbody2D>().velocity.y;
        oldVel = GetComponent<Rigidbody2D>().velocity.y;
        if (velDelta < -3f && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Instantiate(Manager.explode.gameObject, transform.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * 50f, ForceMode2D.Impulse);
            if (parent != null)
            {
                var parentjoint = parent.GetComponent<PhysicRocketPart>().jointConnectors.Find(x => x.obj == GetComponent<Rigidbody2D>());
                parentjoint.obj.AddRelativeForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * 50f, ForceMode2D.Impulse);

                Destroy(parentjoint.hingeJoint);
                transform.parent = null;
            }
            for (int i = 0; i < jointConnectors.Count; i++)
            {
                if (jointConnectors[i].obj != null)
                    jointConnectors[i].obj.AddRelativeForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * 50f, ForceMode2D.Impulse);
                Destroy(jointConnectors[i].hingeJoint);
            }
            Destroy(gameObject);
        }
    }

    public void Detach()
    {
        if (parent != null)
        {
            var parentjoint = parent.GetComponent<PhysicRocketPart>().jointConnectors.Find(x => x.obj == GetComponent<Rigidbody2D>());
            Destroy(parentjoint.hingeJoint);
            transform.parent = null;
            parent = null;
        }
        for (int i = 0; i < jointConnectors.Count; i++)
        {
            Destroy(jointConnectors[i].hingeJoint);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }



    public static void ConnectAllChilds(Transform trs)
    {
        var builder = trs.GetComponent<PartBuilder>();
        for (int i = 0; i < builder.connectPins.Count; i++)
        {
            var toConnect = builder.connectPins[i].parent.GetComponent<Rigidbody2D>();



            var joint = trs.gameObject.AddComponent<FixedJoint2D>();
            //joint.useLimits = true;
            //joint.limits = new JointAngleLimits2D() { min = 0, max = 0 };
            joint.connectedBody = toConnect.GetComponent<Rigidbody2D>();
            toConnect.mass = toConnect.GetComponent<Part>().mass;
            builder.GetComponent<Rigidbody2D>().mass = builder.GetComponent<Part>().mass;

            var conn = new JointConnector() { obj = toConnect, hingeJoint = joint };
            trs.GetComponent<PhysicRocketPart>().jointConnectors.Add(conn);
            toConnect.transform.parent = null;

            ConnectAllChilds(toConnect.transform);
        }
    }
}
