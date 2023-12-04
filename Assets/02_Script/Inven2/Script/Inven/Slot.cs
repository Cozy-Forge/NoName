using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{

    public ItemObject item { get; private set; }
    public Vector2Int point { get; private set; }
    public Rect rt { get; private set; }

    public void SetPoint(Vector2Int point)
    {

        this.point = point;

        rt = new Rect(transform.localPosition.x - 100 / 2,
            transform.localPosition.y - 100 / 2,
            100, 100);
    }
}
