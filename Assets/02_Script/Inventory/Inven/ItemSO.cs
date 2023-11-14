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

    [Multiline]
    [SerializeField] string itemDescription;
    public string ItemDescription => itemDescription;

    [SerializeField] Sprite itemImage;
    public Sprite ItemImage => itemImage;

    [SerializeField] int stackCount;
    public int StackCount => stackCount;

    [SerializeField] Vector2 weight;
    public Vector2 Weight => weight;

    [SerializeField] ItemType itemType;
    public ItemType ItemType => itemType;
}