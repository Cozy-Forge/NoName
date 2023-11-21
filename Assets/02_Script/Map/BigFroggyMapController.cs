using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BigFroggyMapController : MonoBehaviour
{

    [SerializeField] private GameObject pos;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {

            FindObjectOfType<PlayerController>().transform.position = pos.transform.position;

        }

    }

}
