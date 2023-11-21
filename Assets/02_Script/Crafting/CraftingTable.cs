using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour
{
    private static CraftingTable instance;
    public static CraftingTable Instance => instance;

    [SerializeField] Button craftingBtn;
    ItemBoard itemBoard = null;

    public bool enable = false;

    private List<Item> materials = new List<Item>();
    [SerializeField] List<CraftSlot> slots = new List<CraftSlot>(); //���Ը���Ʈ

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
        enable = false;

        transform.GetComponentsInChildren<CraftSlot>(slots); //�κ��丮 ã�ƿ���
    }

    private void Update()
    {
        craftingBtn.enabled = materials.Count != 0;
    }

    /// <summary>
    /// ��� ����Ʈ�� ������ �߰�
    /// </summary>
    /// <param name="material"></param>
    public void AddItemToList(Item material)
    {
        materials.Add(material);
        foreach (CraftSlot slot in slots)
        {
            if (slot.CurrentItem.ItemData.ItemName == material.ItemData.ItemName) //�̹� ������ ���ÿ� �߰�
                if (slot.CurrentStackCount < material.ItemData.StackCount)
                {
                    slot.IncreaseItem();
                    break;
                }
            if (slot.CurrentItem.ItemData.ItemName == "NoneItem") //������ ����
            {
                slot.SetItem(material, 1);
                break;
            }
        }
    }

    /// <summary>
    /// ������ ��� ��
    /// </summary>
    /// <returns></returns>
    public void Crafting()
    {
        Vector3 curPos = Vector3.zero;

        foreach (var mat in materials)
        {
            curPos += mat.ItemData.Weight;
        }
        Item result = itemBoard.GetNearItem(curPos);
        materials.Clear();
        foreach (CraftSlot slot in slots)
        {
            slot.SetItem(Inventory.instance.NoneItem, Inventory.instance.NoneItem.ItemData.StackCount);
        }
        OpenTetris(result);
    }

    private void OpenTetris(Item result)
    {
        InventoryButtonManager.Instance.CompulsionOpenTetris();
        BlockManager.Instance.CreateBlock(result.tetrisImg);
    }
}