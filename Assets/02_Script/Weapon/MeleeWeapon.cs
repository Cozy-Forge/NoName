using System;
using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    protected override void DoAttack(Transform trm)
    {
        Debug.Log("근접공격");

    }

}
