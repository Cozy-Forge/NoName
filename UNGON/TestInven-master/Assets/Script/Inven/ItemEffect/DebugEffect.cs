using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Effect/Debug")]
public class DebugEffect : ItemEffectSO
{
    public override void Effect(ItemType type)
    {

        Debug.Log(type);

    }
}
