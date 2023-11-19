using UnityEngine;

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

    [SerializeField] Vector3 weight;
    public Vector3 Weight => weight;

    [SerializeField] ItemType itemType;
    public ItemType ItemType => itemType;
}