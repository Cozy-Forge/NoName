using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{

    private void OnEnable()
    {
        
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerWeaponContainer>().enabled = false;

    }

    private void OnDisable()
    {

        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<PlayerWeaponContainer>().enabled = true;

    }

}
