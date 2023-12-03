using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [field:SerializeField] public ItemSO itemSo { get; private set; }

    private Item item;
    private Inventory inventory;
    private InventoryViewer inventoryViewer;
    private bool isDrag;
    private Vector2 originPos;

    private void Awake()
    {
        
        inventory = FindObjectOfType<Inventory>();
        inventoryViewer = FindObjectOfType<InventoryViewer>();
        item = itemSo.MakeItem();
        originPos = transform.localPosition;

    }

    private void Update()
    {

        if (isDrag)
        {

            transform.position = Input.mousePosition;

        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        isDrag = true;

        inventory.RemoveItem(item);

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        isDrag = false;

        var point = inventoryViewer.GetPoint(transform.localPosition);
        Debug.Log(point);
        if (point != null)
        {

            if(inventory.AddItem(item, point.Value, itemSo.itemSizes))
            {

                transform.localPosition = new Vector2(point.Value.x - inventory.width / 2,
                    point.Value.y - inventory.height / 2) * 100;

            }
            else
            {

                transform.localPosition = originPos;

            }

        }
        else
        {

            transform.localPosition = originPos;

        }
    }

}
