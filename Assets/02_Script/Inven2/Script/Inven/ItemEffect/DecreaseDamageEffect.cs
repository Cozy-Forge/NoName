using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Effect/DecreaseDamage")]
public class DecreaseDamageEffect : ItemEffectSO
{

    public override void Effect(Item item)
    {
        //Debug.Log(item.attackPower);
        item.attackPower += 100;
        //Debug.Log(item.attackPower);
    }
}
