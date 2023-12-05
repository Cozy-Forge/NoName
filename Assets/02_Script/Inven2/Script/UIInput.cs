using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public List<GameObject> objs = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject obj in objs)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
