using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Data")]
public class WeaponDataSO : ScriptableObject
{
    
    [field: SerializeField] public float Range { get; protected set; }
    [field: SerializeField] public float AttackPower { get; protected set; }
    [field: SerializeField] public float AttackCoolDown { get; protected set; }

}
