using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{

    private Item _item;

    private void Awake()
    {
        
        _item = GetComponent<Item>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            Inventory.instance.AddItem(_item);

        }

    }

}
