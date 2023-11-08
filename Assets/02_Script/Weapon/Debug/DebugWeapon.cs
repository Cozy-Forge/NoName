using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeapon : Weapon
{
    protected override void DoAttack()
    {

        Debug.Log(123);

    }

}
