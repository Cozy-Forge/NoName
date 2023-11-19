using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggyController : StateController<EnumBigFroggyState>, IEnemyDataGetAble
{

    [SerializeField] private BigFroggyDataSO _data;

    public EnemyDataSO GetData()
    {

        return _data;

    }

    private void Awake()
    {

        _data = Instantiate(_data);

        #region Idle

        var idleState = new EnemyIdleState<EnumBigFroggyState>(this, _data);

        var idleToJump = new EnemyTargetRangeTransition<EnumBigFroggyState>
            (transform, _data.JumpRange, _data.TargetAbleLayer, EnumBigFroggyState.Skill_Jump, () => !_data.IsJumpCoolDown);

        var idleToFire = new EnemyTargetRangeTransition<EnumBigFroggyState>
            (transform, _data.FireRange, _data.TargetAbleLayer, EnumBigFroggyState.Skill_Fire, () => !_data.IsFireCoolDown);

        idleState
            .AddTransition(idleToJump)
            .AddTransition(idleToFire);

        #endregion

        var jumpState = new FroggyJumpState(this, _data);
        var fireState = new FroggyFireState(this, _data);

        _stateContainer.Add(EnumBigFroggyState.Idle, idleState);
        _stateContainer.Add(EnumBigFroggyState.Skill_Jump, jumpState);
        _stateContainer.Add(EnumBigFroggyState.Skill_Fire, fireState);

        CurrentState = EnumBigFroggyState.Idle;

    }

}
