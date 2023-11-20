using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/Glowy")]
public class LaserMonsterDataSO : EnemyDataSO
{
    [field: Space]
    [field: Header("∑π¿Ã¿˙")]
    [field: SerializeField] public float LaserRange { get; private set; }
    [field: SerializeField] public float LaserTargetingTime { get; private set; }
    [field: SerializeField] public float LaserDelay { get; private set; }
    [field: SerializeField] public float LaserHoldingTime { get; private set; }
    [field: SerializeField] public float LaserDamage { get; private set; }

    [field: SerializeField] public LayerMask WallLayer { get; private set; }
}
