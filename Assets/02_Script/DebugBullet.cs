using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBullet : MonoBehaviour
{

    private void Update()
    {
        
        transform.Translate(transform.right * 10 * Time.deltaTime);

    }

}
