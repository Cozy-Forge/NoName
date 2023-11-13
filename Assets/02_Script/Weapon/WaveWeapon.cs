using FD.Dev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveWeapon : Weapon
{

    private PlayerController _playerController;

    protected override void Awake()
    {

        base.Awake();

        _playerController = FindObjectOfType<PlayerController>();
        _playerController.OnDashEndEvent += SpawnWave;

    }

    private void SpawnWave()
    {
        Instantiate(gameObject,_playerController.transform.position, Quaternion.identity);
    }

    EnemyDataSO _enemyDataSO;

    protected override void DoAttack(Transform trm)
    {
        if(trm.GetComponent<TestEnemyController>().Data.Speed > 2)
        {
            trm.GetComponent<TestEnemyController>().Data.Speed -= 2;
        }
        Debug.Log(trm.GetComponent<TestEnemyController>().Data.Speed);
    }
    

}
