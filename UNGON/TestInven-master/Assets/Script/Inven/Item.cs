using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{

    Gray = 0,
    Red,
    Green,
    Blue,
    Yellow,

}

[System.Serializable]
public struct Item
{

    public int attackPower;
    public int attackSpeed;
    public ItemType type;
    public ItemEffectSO so;
    [HideInInspector] public Guid guid;
    //이외 어쩌고저쩌고 많은 값들
}
