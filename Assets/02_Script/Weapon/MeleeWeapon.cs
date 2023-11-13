using System;
using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{

    public float _swingTime = 0.2f;
    public float _swingDelayTime = 1f;

    protected override void DoAttack(Transform trm)
    {
        Debug.Log("근접공격");

    }


    




}
