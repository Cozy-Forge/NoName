using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemSlot> slots = new List<ItemSlot>(); //슬롯리스트

    [SerializeField] Item noneItem; //빈 슬롯을 표시
    public Item NoneItem => noneItem;

    List<Item> itemList = new List<Item>(); //소지한 아이템 리스트
    public List<Item> ItemList
    {
        get { return itemList; }
        set { itemList = value; }
    }

    private bool saleToggle = true;
    public bool SaleToggle
    {
        get { return saleToggle; }
    }

    public static Inventory instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        transform.GetComponentsInChildren<ItemSlot>(slots); //인벤토리 찾아오기
    }

    public void SaleToggleChange()
    {
        saleToggle = !saleToggle;
        Debug.Log(saleToggle);
    }

    public void RedrawInven()
    {
        foreach (ItemSlot slot in slots)
        {
            slot.SetItem(noneItem, noneItem.ItemData.StackCount);
        }
        Debug.Log(itemList.Count);
        for (int i = 0; i < itemList.Count; i++)
        {
            foreach (ItemSlot slot in slots)
            {
                if (slot.CurrentItem.ItemData.ItemName == itemList[i].ItemData.ItemName) //이미 있으면 스택에 추가
                    if (slot.CurrentStackCount < itemList[i].ItemData.StackCount)
                    {
                        slot.AddItem();
                        break;
                    }
                if (slot.CurrentItem.ItemData.ItemName == "NoneItem") //없으면 생성
                {
                    slot.SetItem(itemList[i], 1);
                    break;
                }
            }
        }
    }

    public void AddItem(Item item) //슬롯에 아이템 추가하기
    {
        itemList.Add(item);
        foreach (ItemSlot slot in slots)
        {
            if (slot.CurrentItem.ItemData.ItemName == item.ItemData.ItemName) //이미 있으면 스택에 추가
                if (slot.CurrentStackCount < item.ItemData.StackCount)
                {
                    slot.AddItem();
                    break;
                }
            if (slot.CurrentItem.ItemData.ItemName == "NoneItem") //없으면 생성
            {
                slot.SetItem(item, 1);
                break;
            }
        }
    }
}