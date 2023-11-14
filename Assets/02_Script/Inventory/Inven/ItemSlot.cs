using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] Item currentItem = null;

    public Item CurrentItem => currentItem;

    private int currentStackCount = 0;
    public int CurrentStackCount => currentStackCount;

    private TextMeshProUGUI stackText = null;
    private TextMeshProUGUI itemName = null;
    private TextMeshProUGUI itemDescription = null;
    private Image image = null;

    private void Awake()
    {
        stackText = GetComponentInChildren<TextMeshProUGUI>();
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = currentItem.ItemData.ItemImage;
    }

    /// <summary>
    /// 슬롯 클릭시 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void AddItem()
    {
        currentStackCount++;
        stackText.text = $"{currentStackCount}";
    }

    public void SetItem(Item item, int count)
    {
        currentItem = item;
        currentStackCount = count;

        image.sprite = currentItem.ItemData.ItemImage;

        stackText.text = currentStackCount == 0 ? string.Empty : $"{currentStackCount}";
    }
}
