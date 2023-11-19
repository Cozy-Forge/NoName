using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    private static CraftingTable instance;
    public static CraftingTable Instance => instance;

    ItemBoard itemBoard = null;

    public List<Item> materials = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple Crafting Table Instance Running");
        }
        itemBoard = GetComponent<ItemBoard>();
    }

    /// <summary>
    /// ��� ����Ʈ�� ������ �߰�
    /// </summary>
    /// <param name="material"></param>
    public void AddItemToList(Item material)
    {
        materials.Add(material);
    }

    /// <summary>
    /// ������ ��� ��
    /// </summary>
    /// <returns></returns>
    public Item Crafting()
    {
        Vector3 curPos = Vector3.zero;

        foreach (var mat in materials)
        {
            curPos += mat.ItemData.Weight;
        }

        return itemBoard.GetNearItem(curPos);
    }
}
