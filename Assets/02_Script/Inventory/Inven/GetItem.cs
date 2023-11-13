using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("FieldItem"))
        {
            item = collision.transform.GetComponent<Item>();
            //Debug.Log(collision.name);
            Inventory.instance.AddItem(item);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("FieldItem"))
        {
            item = collision.transform.GetComponent<Item>();
            //Debug.Log(collision.name);
            Inventory.instance.AddItem(item);
        }
    }
}
