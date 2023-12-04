using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Effect/IncreaseDamage")]
public class IncreaseDamageEffect : ItemEffectSO
{
    public override void Effect(Item item)
    {
        item.attackPower += 3;
    }
}
