using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{

    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float AttackPower { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float AttackAbleRange { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float AttackCoolDown { get; private set; }
    [field: SerializeField] public LayerMask TargetAbleLayer { get; private set; }

    public bool IsCoolDown { get; private set; }

    public void SetCoolDown()
    {

        if (IsCoolDown) return;
        IsCoolDown = true;

        FAED.InvokeDelay(() =>
        {

            IsCoolDown = false;

        }, AttackCoolDown);

    }

}
