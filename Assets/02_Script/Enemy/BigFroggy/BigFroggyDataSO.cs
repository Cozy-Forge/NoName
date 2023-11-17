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

}
