using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{

    [SerializeField] private PlayerInputReader _reader;

    private void OnEnable()
    {
        
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerWeaponContainer>().enabled = false;
        FindObjectOfType<Rigidbody2D>().velocity = Vector2.zero;
        _reader.InputData.Disable();
    }

    private void OnDisable()
    {

        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<PlayerWeaponContainer>().enabled = true;
        FindObjectOfType<Rigidbody2D>().velocity = Vector2.zero;
        _reader.InputData.Enable();

    }

}
