using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDataChangeWeapon : Weapon
{

    protected Item item;

    public void SetItemData(Item item)
    {

        this.item = item;

    }

}
