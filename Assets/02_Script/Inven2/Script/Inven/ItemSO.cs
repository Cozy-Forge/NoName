using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/ItemData")]
public class ItemSO : ScriptableObject
{

    [SerializeField] private Item item;

    //계산 기준은 스프라이트의 피봇
    [field: SerializeField] public List<Vector2Int> itemSizes { get; private set; } = new();

    public Item MakeItem()
    {

        Item copy = new Item
        {

            attackPower = item.attackPower,
            attackDelay = item.attackDelay,
            effectSO = Instantiate(item.effectSO),
            type = item.type,
            guid = Guid.NewGuid(),
            weapon = item.weapon,

        };

        return copy;

    }
}