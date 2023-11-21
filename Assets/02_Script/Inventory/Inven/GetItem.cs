using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{

    private Item _item;

    private void Awake()
    {
        
        _item = GetComponent<Item>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            Inventory.instance.AddItem(_item);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.transform.CompareTag("Player"))
        {
            Inventory.instance.AddItem(_item);
            Destroy(gameObject);
        }

    }
}
