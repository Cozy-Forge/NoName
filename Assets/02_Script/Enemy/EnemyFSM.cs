using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefaultEnemyState
{

    Idle,
    Move,
    Attack,
    Die

}


public abstract class EnemyState : State<DefaultEnemyState>
{
    protected EnemyState(StateController<DefaultEnemyState> controller, EnemyDataSO data) : base(controller)
    {

        _data = data;

    }

    protected EnemyDataSO _data;

}

#region States

public class EnemyIdleState : EnemyState
{

    public EnemyIdleState(StateController<DefaultEnemyState> controller, EnemyDataSO data) : base(controller, data)
    {
    }

    public override void Create()
    {

        var rangeTransition = new EnemyTargetRangeTransition(_transform, _data.Range, _data.TargetAbleLayer, DefaultEnemyState.Move);
        _transitions.Add(rangeTransition);

    }

    protected override void Run()
    {



    }

}

public class EnemyMoveState : EnemyState
{

    public EnemyMoveState(StateController<DefaultEnemyState> controller, EnemyDataSO data) : base(controller, data)
    {
    }

    private Transform _target;
    private Rigidbody2D _rigid;

    public override void Create()
    {

        _rigid = _transform.GetComponent<Rigidbody2D>();

    }

    protected override void Run()
    {

        FindTarget();

        if (_isControlReleased) return;

        Move();

    }

    private void Move()
    {

        var dir = _target.position - _transform.position;
        dir.Normalize();

        _rigid.velocity = dir * _data.Speed;

    }

    private void FindTarget()
    {

        var hit = Physics2D.OverlapCircle(_transform.position, _data.Range, _data.TargetAbleLayer);

        if(hit != null)
        {

            _target = hit.transform;

        }
        else
        {

            _controller.ChangeState(DefaultEnemyState.Idle);

        }

    }

}

#endregion

#region Transition

public class EnemyTargetRangeTransition : Transition<DefaultEnemyState>
{

    public EnemyTargetRangeTransition(Transform transform,float range, LayerMask targetLayer, DefaultEnemyState nextState) : base(nextState)
    {

        _range = range;
        _targetLayer = targetLayer;
        _transform = transform;

    }

    private Transform _transform;
    private float _range;
    private LayerMask _targetLayer;

    public override bool ChackTransition()
    {

        return Physics2D.OverlapCircle(_transform.position, _range, _targetLayer);

    }

}

public class EnemyAttackTransition : EnemyTargetRangeTransition
{
    public EnemyAttackTransition(EnemyDataSO data,Transform transform, float range, LayerMask targetLayer, DefaultEnemyState nextState) : base(transform, range, targetLayer, nextState)
    {

        _data = data;

    }

    private EnemyDataSO _data;

    public override bool ChackTransition()
    {

        return _data.IsAttackCoolDown && base.ChackTransition();

    }

}

#endregion