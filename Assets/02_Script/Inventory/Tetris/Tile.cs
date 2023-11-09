using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    int _x;
    int _y;
    public Vector2 GetXY()
    {
        return new Vector2(_x,_y);
    }
}
