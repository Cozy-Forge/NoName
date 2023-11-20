using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    [Header("프리팹으로 None넣어놓기")]
    [SerializeField] Item currentItem = null;
    public Item CurrentItem => currentItem;

    private int currentStackCount = 0;
    public int CurrentStackCount => currentStackCount;

    private Image image = null;
    private TextMeshProUGUI stackText = null;

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        stackText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
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
        stackText.text = currentStackCount == 0 ? string.Empty : $"{currentStackCount}"; // 스택 카운트
    }
}
