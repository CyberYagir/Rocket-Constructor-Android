using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinType : MonoBehaviour
{
    public enum Type { Up, Down, Left, Right};
    public Type type;

    public bool Check(PinType pinType)
    {
        if (type == Type.Up && pinType.type == Type.Down)
        {
            return true;
        }
        if (type == Type.Down && pinType.type == Type.Up)
        {
            return true;
        }
        if (type == Type.Left && pinType.type == Type.Right)
        {
            return true;
        }
        if (type == Type.Right && pinType.type == Type.Left)
        {
            return true;
        }
        return false;
    }
}
