using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{

    [SerializeField] private PlayerInputReader _reader;

    private GameObject pl;

    private void OnEnable()
    {

        pl = FindObjectOfType<PlayerController>().gameObject;
        pl.SetActive(false);

        _reader.InputData.Disable();
    }

    private void OnDisable()
    {
        
        if(pl != null)
        {

            pl.SetActive(true);

        }

        _reader.InputData.Enable();

    }

}
