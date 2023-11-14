using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler
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
        Debug.Log(1);
        image.sprite = currentItem.ItemData.ItemImage; // 이미지
        itemName.text = currentItem.ItemData.ItemName; // 이름
        itemDescription.text = currentItem.ItemData.ItemDescription; // 설명
        stackText.text = currentStackCount == 0 ? string.Empty : $"{currentStackCount}"; // 스택 카운트
    }
}
