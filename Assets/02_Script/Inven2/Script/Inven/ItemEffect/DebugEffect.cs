using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Effect/Debug")]
public class DebugEffect : ItemEffectSO
{
    public override void Effect(Item item)
    {
        Debug.Log(item.name);
    }
}
