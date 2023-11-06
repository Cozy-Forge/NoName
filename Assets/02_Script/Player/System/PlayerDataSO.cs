using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player/Data")]
public class PlayerDataSO : ScriptableObject
{

    public float MoveSpeed { get; private set; }

}
