using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveWeapon : Weapon
{
    EnemyDataSO _enemyDataSO;
    protected override void DoAttack(Transform trm)
    {
        trm.GetComponent<TestEnemyController>().Data.Speed = 0;
        Debug.Log("Dd");
    }

}
