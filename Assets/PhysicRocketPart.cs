﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class JointConnector{
    public Rigidbody2D obj;
    public FixedJoint2D hingeJoint;
}

public class PhysicRocketPart : MonoBehaviour
{
    public Transform parent;
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
            Destroy(Instantiate(Manager.explode.gameObject, transform.position, Quaternion.identity), 20f);
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
            Player.money -= GetComponent<Part>().cost;
            Destroy(gameObject);
        }
    }

    public void Detach()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Random.Range(-2, 2), Random.Range(-0.1f, -2)), ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(1, ForceMode2D.Impulse);
        if (GetComponent<Thruster>())
        {
            GetComponent<Thruster>().run = false;
        }

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



    public void ConnectAllChilds()
    {
        var builder = GetComponent<PartBuilder>();
        transform.parent = null;
        GetComponent<Rigidbody2D>().mass = GetComponent<Part>().mass;

        for (int i = 0; i < builder.connectPoints.Count; i++)
        {
            var connected = builder.connectPoints[i].connectPin.parent;
            var connectedJoints = connected.GetComponents<HingeJoint2D>().ToList();
            if (connectedJoints.Find(x=>x.connectedBody == GetComponent<Rigidbody2D>()) == null)
            {
                var joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = connected.GetComponent<Rigidbody2D>();

                var conn = new JointConnector() { obj = connected.GetComponent<Rigidbody2D>(), hingeJoint = joint };
                jointConnectors.Add(conn);


            }
        }
    }
}
