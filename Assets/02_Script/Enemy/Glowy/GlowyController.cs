using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumGlowyState
{
    Idle, Move, Laser
}

public class GlowyController : StateController<EnumGlowyState>
{
    [SerializeField] private GlowyDataSO _data;

    private void Awake()
    {
        _data = Instantiate(_data);

        #region Idle

        //Idle
        var idleState = new EnemyIdleState<EnumGlowyState>(this, _data);

        var idleToJump = new EnemyTargetRangeTransition<EnumGlowyState>
            (transform, _data.Range, _data.TargetAbleLayer, EnumGlowyState.Move);

        idleState
            .AddTransition(idleToJump);

        #endregion

        #region Move

        //Move
        var moveState = new EnemyMoveState<EnumGlowyState>(EnumGlowyState.Idle, this, _data);

        var moveToLaser = new EnemyTargetRangeTransition<EnumGlowyState>
            (transform, _data.AttackAbleRange, _data.TargetAbleLayer, EnumGlowyState.Laser, () => !_data.IsAttackCoolDown);

        var moveToLaserLengthCheck = new EnemyTargetRangeTransition<EnumGlowyState>
            (transform, _data.AttackAbleRange, _data.TargetAbleLayer, EnumGlowyState.Idle);
        var moveToIdle = new ReverseTransition<EnumGlowyState>(moveToLaserLengthCheck);

        moveState
            .AddTransition(moveToLaser)
            .AddTransition(moveToIdle);
            
        #endregion

        var laserState = new GlowyLaserState(this, _data);

        _stateContainer.Add(EnumGlowyState.Idle, idleState);
        _stateContainer.Add(EnumGlowyState.Move, moveState);
        _stateContainer.Add(EnumGlowyState.Laser, laserState);

        CurrentState = EnumGlowyState.Idle;
    }
}
