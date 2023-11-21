using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{

    [SerializeField] private GameObject player;

    public void Pos(Vector3 pos)
    {

        player.transform.position = pos;
        player.SetActive(true);

    }

}
