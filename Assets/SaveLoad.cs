using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class SaveLoad : MonoBehaviour
{
    public string filePath;
    public string worldname;

    private void Start()
    {
        filePath = Application.persistentDataPath;
    }
    public void Save()
    {

        var world = new World();

        var conns = new List<Connections>(); 
        var mainPart = UIManager.manager.turret.GetComponent<TurretHandle>().mainRocket.GetComponent<Part>();

        foreach (var it in mainPart.GetComponent<PartBuilder>().connectPoints)
        {
            conns.Add(new Connections() { connectedObjectCode = it.connectPin.parent.GetComponent<Part>().partCode.ToString(), type = it.connectPin.GetComponent<PinType>().type});
        }
        
        world.mainPart = (new PartRocket() { partName = mainPart.partName, pos = Vector2C.from(mainPart.transform.position), uniq = mainPart.partCode, connectedUniqs = conns});
        foreach (var item in FindObjectsOfType<Part>())
        {
            if (mainPart != item)
            {
                conns = new List<Connections>();
                foreach (var it in item.GetComponent<PartBuilder>().connectPoints)
                {
                    conns.Add(new Connections() { connectedObjectCode = it.connectPin.parent.GetComponent<Part>().partCode.ToString(), type = it.connectPin.GetComponent<PinType>().type });
                }
                world.parts.Add(

                    new PartRocket()
                    {
                        partName = item.partName,
                        pos = Vector2C.from((Vector2)item.transform.position),
                        uniq = item.partCode,
                        connectedUniqs = conns
                    });
            }
        }
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream($"{filePath}/{worldname}.yWorld", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, world);
        }
    }

    public void Load()
    {

        var world = new World();

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream($"{filePath}/{worldname}.yWorld", FileMode.OpenOrCreate))
        {
            world =  (World)formatter.Deserialize(fs);
        }
        foreach (var item in FindObjectsOfType<Part>())
        {
            Destroy(item.gameObject);
        }
        var prefabs = Resources.FindObjectsOfTypeAll<Part>();

        var mainPart = (GameObject)Instantiate(Resources.Load("Parts/" + world.mainPart.partName), world.mainPart.pos.to(), Quaternion.identity);
        mainPart.GetComponent<Part>().randomName = false;
        mainPart.GetComponent<Part>().partCode = world.mainPart.uniq;
        mainPart.GetComponent<PartBuilder>().FindConnections(world.mainPart.connectedUniqs);

        UIManager.manager.turret.GetComponent<TurretHandle>().mainRocket = mainPart.GetComponent<Part>();
        for (int i = 0; i < world.parts.Count; i++)
        {
            var n = (GameObject)Instantiate(Resources.Load("Parts/" + world.parts[i].partName), world.parts[i].pos.to(), Quaternion.identity);
            n.GetComponent<Part>().randomName = false;
            n.GetComponent<Part>().partCode = world.parts[i].uniq;
        }
        foreach (var item in FindObjectsOfType<PartBuilder>())
        {
            var p = world.parts.Find(x => x.uniq == item.GetComponent<Part>().partCode);
            if (p != null)
            {
                item.FindConnections(p.connectedUniqs);
            }
        }

    }

    [System.Serializable]
    public class World{
        public string name;
        public PartRocket mainPart;
        public List<PartRocket> parts = new List<PartRocket>();

    }

    [System.Serializable]
    public class Vector2C
    {
        public float x, y;
        public Vector2C(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 to()
        {
            return new Vector2(x, y);
        }
        public static Vector2C from(Vector2 vec)
        {
            return new Vector2C(vec.x, vec.y);
        }
    }
    [System.Serializable]
    public class PartRocket
    {
        public string partName, uniq;
        public List<Connections> connectedUniqs = new List<Connections>();
        public Vector2C pos = new Vector2C(0, 0);
    }

    [System.Serializable]
    public class Connections
    {
        public PinType.Type type;
        public string connectedObjectCode;
    }
}
