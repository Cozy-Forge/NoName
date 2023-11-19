using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour
{

    private IEnumerator Start()
    {
        
        yield return null;
        FindObjectOfType<PlayerController>().transform.position = transform.position;

    }

}
