using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumGlowyState
{
    Idle, Move, Laser
}

public class GlowyController : StateController<EnumGlowyState>
{
    [SerializeField] private LaserMonsterDataSO _data;
    [SerializeField] private LineRenderer _lineRenderer;

    private void Awake()
    {
        _data = Instantiate(_data);

        #region Idle

        //Idle
        var idleState = new EnemyIdleState<EnumGlowyState>(this, _data);

        var idleToMove = new EnemyTargetRangeTransition<EnumGlowyState>
            (transform, _data.Range, _data.TargetAbleLayer, EnumGlowyState.Move);

        idleState
            .AddTransition(idleToMove);

        #endregion

        #region Move

        //Move
        var moveState = new EnemyMoveState<EnumGlowyState>(EnumGlowyState.Idle, this, _data);

        var moveToLaser = new EnemyTargetRangeTransition<EnumGlowyState>
            (transform, _data.AttackAbleRange, _data.TargetAbleLayer, EnumGlowyState.Laser, () => !_data.IsAttackCoolDown);

        var moveToIdle = new ReverseTransition<EnumGlowyState>(idleToMove);

        moveState
            .AddTransition(moveToLaser)
            .AddTransition(moveToIdle);
            
        #endregion

        var laserState = new GlowyLaserState(this, _data, _lineRenderer);

        _stateContainer.Add(EnumGlowyState.Idle, idleState);
        _stateContainer.Add(EnumGlowyState.Move, moveState);
        _stateContainer.Add(EnumGlowyState.Laser, laserState);

        CurrentState = EnumGlowyState.Idle;
    }
}
