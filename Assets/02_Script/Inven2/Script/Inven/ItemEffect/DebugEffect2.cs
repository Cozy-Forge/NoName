using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Effect/Debug2")]
public class DebugEffect2 : ItemEffectSO
{
    public override void Effect(ItemType type)
    {
        Debug.Log("222");
    }
}
