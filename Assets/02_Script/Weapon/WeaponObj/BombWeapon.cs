using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HPObject>().TakeDamage(20);
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
