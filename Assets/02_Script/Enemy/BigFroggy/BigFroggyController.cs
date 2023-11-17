using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumBigFroggyState
{

    Idle,
    Skill_Jump,
    Skill_Fire,
    Skill_Cyclorrhapha

}

public class BigFroggyController : StateController<EnumBigFroggyState>
{

    [SerializeField] private BigFroggyDataSO _data;

    private void Awake()
    {
        
        var idleState = new EnemyIdleState<EnumBigFroggyState>(this, _data);

    }

}
