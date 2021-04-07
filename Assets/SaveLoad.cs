using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{


    public void Save()
    {

    }

    public void Load()
    {

    }


    public class World{
        public string name;
        public List<Part> parts;

    }

    public class Vector2C
    {
        public float x, y;
        public Vector2C(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public  Vector2 to()
        {
            return new Vector2(x, y);
        }
        public void from(Vector2 vec)
        {
            x = vec.x;
            y = vec.y;
        }
    }
    public class Part
    {
        public string partName;
        public List<Part> parts;
        public Vector2C pos;
    }
}
