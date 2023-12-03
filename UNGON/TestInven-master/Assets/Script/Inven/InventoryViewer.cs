using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewer : MonoBehaviour
{

    [SerializeField] private Slot slotPrefab;

    private Inventory inventory;
    private List<Slot> container = new();


    private void Awake()
    {
        inventory = GetComponent<Inventory>();

        for(int y = -inventory.height / 2; y <= inventory.height / 2; y++)
        {

            for(int x = -inventory.width / 2; x <= inventory.width / 2; x++)
            {

                var slot = Instantiate(slotPrefab, transform);
                slot.transform.localPosition = new Vector3(x, y, 0) * 100;
                slot.SetPoint(new Vector2Int(x + inventory.width / 2, y + inventory.height / 2));
                container.Add(slot);

            }

        }

    }

    public Vector2Int? GetPoint(Vector2 point)
    {

        var target = container.Find(x => x.rt.Contains(point));

        if(target != null)
        {

            return target.point;

        }

        return null;

    }

}
