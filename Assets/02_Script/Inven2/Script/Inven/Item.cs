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

// ���Ÿ� �ٰŸ� ���
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
    //�̿� ��¼����¼�� ���� ����
}

