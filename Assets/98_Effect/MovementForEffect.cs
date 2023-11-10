using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForEffect : MonoBehaviour
{
    private float speed = 5;
    Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0).normalized * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void Dash()
    {

    }
}
