using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BigFroggyState
{

    Idle,
    Skill_Jump,
    Skill_Fire,
    Skill_Cyclorrhapha

}

public class BigFroggyController : StateController<BigFroggyState>
{

    [SerializeField] private BigFroggyDataSO _data;

    private void Awake()
    {
        
        var idleState = new EnemyIdleState<BigFroggyState>(this, _data);

    }

}
