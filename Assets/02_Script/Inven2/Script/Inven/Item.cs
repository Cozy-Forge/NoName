using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType
{

    Melee,
    Range,
    Accessory,

}

// 원거리 근거리 등등
public enum Category
{


}

[System.Serializable]
public class Item
{
    public string name;
    public int attackPower;
    public float attackDelay;
    public ItemType type;
    public ItemEffectSO effectSO;
    [HideInInspector] public Guid guid;
    //이외 어쩌고저쩌고 많은 값들
}

