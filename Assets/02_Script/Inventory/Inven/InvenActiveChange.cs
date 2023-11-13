using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenActiveChange : MonoBehaviour
{
    [SerializeField] GameObject ui;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ReverseActive();
        }
    }

    public void ReverseActive()
    {
        ui.SetActive(!ui.activeSelf);
    }
}
