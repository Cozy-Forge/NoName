using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [Header("프리팹으로 None넣어놓기")]
    [SerializeField] Item currentItem = null;

    public Item CurrentItem => currentItem;

    private int currentStackCount = 0;
    public int CurrentStackCount => currentStackCount;

    private Image image = null;
    private TextMeshProUGUI itemName = null;
    private TextMeshProUGUI itemDescription = null;
    private TextMeshProUGUI stackText = null;

    private void Awake()
    {
        image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemDescription = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        stackText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        SetItem(currentItem, currentStackCount);
    }

    /// <summary>
    /// 슬롯 클릭시 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        CraftingTable.Instance.AddItemToList(currentItem);
        RemoveItem();
        Debug.Log("누름");
    }

    public void RemoveItem()
    {
        if (currentStackCount > 0)
        {
            currentStackCount--;
            Inventory.instance.ItemList.Remove(currentItem);

            if (currentStackCount <= 0)
            {
                Item noneItem = Inventory.instance.NoneItem;
                SetItem(noneItem, noneItem.ItemData.StackCount);
            }
        }
        stackText.text = currentStackCount == 0 ? string.Empty : $"{currentStackCount}";
    }

    public void IncreaseItem()
    {
        currentStackCount++;
        stackText.text = $"{currentStackCount}";
    }

    public void SetItem(Item item, int count)
    {
        currentItem = item;
        currentStackCount = count;
        image.sprite = currentItem.ItemData.ItemImage; // 이미지
        itemName.text = currentItem.ItemData.ItemName; // 이름
        itemDescription.text = currentItem.ItemData.ItemDescription; // 설명
        stackText.text = currentStackCount == 0 ? string.Empty : $"{currentStackCount}"; // 스택 카운트
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CraftingTable.Instance.AddItemToList(currentItem);
        RemoveItem();
        Debug.Log("누름");
    }
}