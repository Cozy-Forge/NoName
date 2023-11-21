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
        else if (other.CompareTag("Hit"))
        {
            other.GetComponent<Hitbox>().Casting(20);
        }
        else
            Debug.Log("null");
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
