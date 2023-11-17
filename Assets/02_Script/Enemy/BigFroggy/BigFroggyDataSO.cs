using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/BigFroggy")]
public class BigFroggyDataSO : EnemyDataSO
{

    [field: Space]
    [field: Header("มกวม")]
    [field: SerializeField] public float JumpRange { get; private set; }
    [field: SerializeField] public float JumpPower { get; private set; }
    [field: SerializeField] public float JumpDuration { get; private set; }
    [field: SerializeField] public float JumpCoolDown { get; private set; }
    [field: SerializeField] public float LandBulletCount { get; private set; }


    public bool IsJumpCoolDown { get; private set; }

    public void SetJumpCoolDown()
    {

        if (IsJumpCoolDown) return;
        IsJumpCoolDown = true;

        FAED.InvokeDelay(() =>
        {

            IsJumpCoolDown = false;

        }, JumpCoolDown);

    }

}
