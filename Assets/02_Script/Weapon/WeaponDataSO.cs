using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponData
{

    public float Range;
    public float AttackPower;
    public float AttackCoolDown;

}

[CreateAssetMenu(menuName = "SO/Weapon/Data")]
public class WeaponDataSO : ScriptableObject
{
    
    [field:SerializeField] public WeaponData Data { get; protected set; }

}
