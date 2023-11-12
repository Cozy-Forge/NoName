using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    protected override void DoAttack(Transform trm)
    {

        Debug.Log("근접공격");

    }


}
