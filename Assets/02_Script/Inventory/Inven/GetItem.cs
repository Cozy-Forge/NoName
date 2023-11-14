using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody.TryGetComponent<Item>(out Item item))
        {
            Inventory.instance.AddItem(item);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<Item>(out Item item))
        {
            Inventory.instance.AddItem(item);
        }
    }
}
