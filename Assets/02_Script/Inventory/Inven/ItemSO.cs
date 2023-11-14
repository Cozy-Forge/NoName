using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ItemType
{
    Material,
    Weapon,
    None
}

[CreateAssetMenu(menuName = "SO/Item/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string itemName;
    public string ItemName => itemName;

    [SerializeField] Sprite itemImage;
    public Sprite ItemImage => itemImage;

    [SerializeField] int stackCount;
    public int StackCount => stackCount;

    [SerializeField] ItemType itemType;
    public ItemType ItemType => itemType;
}