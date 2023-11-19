using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler
{
    [Header("���������� None�־����")]
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
    /// ���� Ŭ���� �̺�Ʈ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        CraftingTable.Instance.AddItemToList(currentItem);

    }

    public void RemoveItem()
    {
        if (currentStackCount > 0)
        {
            currentStackCount--;
            Inventory.instance.ItemList.Remove(currentItem);

            if (currentStackCount <= 0)
            {
                currentItem = Inventory.instance.NoneItem;
                image.sprite = currentItem.ItemData.ItemImage;
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
        Debug.Log(1);
        image.sprite = currentItem.ItemData.ItemImage; // �̹���
        itemName.text = currentItem.ItemData.ItemName; // �̸�
        itemDescription.text = currentItem.ItemData.ItemDescription; // ����
        stackText.text = currentStackCount == 0 ? string.Empty : $"{currentStackCount}"; // ���� ī��Ʈ
    }
}