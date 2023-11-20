using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBombWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HPObject>().TakeDamage(30);
        }
        else
        {
            Debug.Log("null");
        }
    }
}
