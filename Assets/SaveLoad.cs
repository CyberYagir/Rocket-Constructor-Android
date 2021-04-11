using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public string filePath;
    public string worldname;
    public int parentID;
    public List<PartBuilder> partBuilders;
    public List<Part> parts;
    public List<PartRocket> partsRockets;
    public List<Connections> conns = new List<Connections>();
    public TMP_InputField inputField;
    private void Start()
    {
        filePath = Application.persistentDataPath;
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("WorldLoad"))
        {
            worldname = PlayerPrefs.GetString("WorldLoad");
            inputField.text = worldname;
            Load(false);
            PlayerPrefs.DeleteKey("WorldLoad");
        }
    }
    public void SetID(PartBuilder part, World world)
    {
        print(part.gameObject.name + ": " + parentID);
        conns = new List<Connections>();
        foreach (var it in part.connectPoints)
        {
            conns.Add(new Connections() { connectedObjectCode = it.connectPin.parent.GetComponent<Part>().partCode.ToString(), type = it.objectPin.GetComponent<PinType>().type });
        }
        world.parts.Add(new PartRocket()
        {
            id = parentID,
            pos = Vector2C.from(part.transform.position),
            partName = part.GetComponent<Part>().partName,
            uniq = part.GetComponent<Part>().partCode,
            connectedUniqs = conns

        });
        parentID++;
        partBuilders.Add(part);
        foreach (var item in part.connectPoints)
        {
            if (!partBuilders.Contains(item.connectPin.parent.GetComponent<PartBuilder>()))
                SetID(item.connectPin.parent.GetComponent<PartBuilder>(), world);
        }
    }

    public void Save()
    {
        worldname = inputField.text;
        if (worldname.Trim() == "") { inputField.placeholder.GetComponent<TMP_Text>().text = LangsList.GetWord("Enter save name!"); return; }


        var world = new World();

        world.money = Player.money;
        var mainPart = UIManager.manager.turret.GetComponent<TurretHandle>().mainRocket.GetComponent<Part>();

        world.mainPart = (new PartRocket() { partName = mainPart.partName, pos = Vector2C.from(mainPart.transform.position), uniq = mainPart.partCode, connectedUniqs = conns});
        
        partBuilders = new List<PartBuilder>();
        SetID(mainPart.GetComponent<PartBuilder>(), world);

        foreach (var item in FindObjectOfType<GroupsManager>().groups)
        {
            var codes = new List<string>(); 
            for (int i = 0; i < item.parts.Count; i++)
            {
                codes.Add(item.parts[i].partCode);
            }
            world.groups.Add(new Group() { parts = codes });
        }



        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream($"{filePath}/{worldname}.yWorld", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, world);
        }
    }

    public void Load(bool button)
    {

        worldname = inputField.text;
        if (button)
            if (worldname.Trim() == "") { inputField.placeholder.GetComponent<TMP_Text>().text = LangsList.GetWord("Enter save name!"); ;  return; }
        
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

        Player.money = world.money;
        world.parts = world.parts.OrderBy(x => x.id).ToList();
        parts = new List<Part>();
        for (int i = 0; i < world.parts.Count; i++)
        {
            if (i == 0)
            {
                var mainPart = (GameObject)Instantiate(Resources.Load("Parts/" + world.parts[i].partName), world.mainPart.pos.to(), Quaternion.identity);
                mainPart.GetComponent<Part>().randomName = false;
                mainPart.GetComponent<Part>().partCode = world.mainPart.uniq; 
                UIManager.manager.turret.GetComponent<TurretHandle>().mainRocket = mainPart.GetComponent<Part>();
                parts.Add(mainPart.GetComponent<Part>());
                continue;
            }
            var n = (GameObject)Instantiate(Resources.Load("Parts/" + world.parts[i].partName), world.parts[i].pos.to(), Quaternion.identity);
            n.GetComponent<Part>().randomName = false;
            n.GetComponent<Part>().partCode = world.parts[i].uniq;
            parts.Add(n.GetComponent<Part>());
        }
        parts.Reverse();
        foreach (var item in parts)
        {
            var p = world.parts.Find(x => x.uniq == item.GetComponent<Part>().partCode);
            if (p != null)
            {
                item.GetComponent<PartBuilder>().FindConnections(p.connectedUniqs);
            }
        }


        var groupsM = FindObjectOfType<GroupsManager>();

        groupsM.groups = new List<global::Group>();

        for (int i = 0; i < world.groups.Count; i++)
        {
            var partsGroup = new List<Part>();
            for (int j = 0; j < world.groups[i].parts.Count; j++)
            {
                partsGroup.Add(parts.Find(x => x.partCode == world.groups[i].parts[j]));
            }
            groupsM.groups.Add(new global::Group() { parts = partsGroup, detach = false });
        }
        groupsM.UpdateList();
    }

    [System.Serializable]
    public class World{
        public float money;
        public PartRocket mainPart;
        public List<PartRocket> parts = new List<PartRocket>();
        public List<Group> groups = new List<Group>();
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
        public int id;
        public List<Connections> connectedUniqs = new List<Connections>();
        public Vector2C pos = new Vector2C(0, 0);
    }

    [System.Serializable]
    public class Connections
    {
        public PinType.Type type;
        public string connectedObjectCode;
    }

    [System.Serializable]
    public class Group {
        public List<string> parts = new List<string>();
    }

}
