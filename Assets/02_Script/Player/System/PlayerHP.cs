using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HPObject
{

    public bool IsInvincibility { get; set; }

    public override void TakeDamage(float damage)
    {

        if (IsInvincibility) return;

        base.TakeDamage(damage);

    }

}
