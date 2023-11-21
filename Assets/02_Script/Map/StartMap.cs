using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour
{

    private IEnumerator Start()
    {

        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<StartController>().Pos(transform.position);

    }

}
