using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeapon : Weapon
{
    protected override void DoAttack(Transform trm)
    {

        Debug.Log(123);

    }

}
