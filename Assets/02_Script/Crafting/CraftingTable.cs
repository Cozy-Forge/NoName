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
    [SerializeField] List<CraftSlot> slots = new List<CraftSlot>(); //슬롯리스트

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

        transform.GetComponentsInChildren<CraftSlot>(slots); //인벤토리 찾아오기
    }

    private void Update()
    {
        craftingBtn.enabled = materials.Count != 0;
    }

    /// <summary>
    /// 재료 리스트에 아이템 추가
    /// </summary>
    /// <param name="material"></param>
    public void AddItemToList(Item material)
    {
        materials.Add(material);
        foreach (CraftSlot slot in slots)
        {
            if (slot.CurrentItem.ItemData.ItemName == material.ItemData.ItemName) //이미 있으면 스택에 추가
                if (slot.CurrentStackCount < material.ItemData.StackCount)
                {
                    slot.IncreaseItem();
                    break;
                }
            if (slot.CurrentItem.ItemData.ItemName == "NoneItem") //없으면 생성
            {
                slot.SetItem(material, 1);
                break;
            }
        }
    }

    /// <summary>
    /// 아이템 뱉는 애
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