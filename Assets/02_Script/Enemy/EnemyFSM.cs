using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class EnemyState<T> : State<T> where T : System.Enum
{
    protected EnemyState(StateController<T> controller, EnemyDataSO data) : base(controller)
    {

        _data = data;

    }

    protected EnemyDataSO _data;

    public EnemyState<T> AddTransition(Transition<T> transition)
    {

        _transitions.Add(transition);
        return this;

    }

}

#region States

public class EnemyIdleState<T> : EnemyState<T> where T : System.Enum
{

    public EnemyIdleState(StateController<T> controller, EnemyDataSO data) : base(controller, data)
    {
    }


    protected override void Run()
    {



    }



}

public class EnemyMoveState<T> : EnemyState<T> where T : System.Enum
{

    public EnemyMoveState(T idleState, StateController<T> controller, EnemyDataSO data) : base(controller, data)
    {

        _idleState = idleState;

    }

    private Transform _target;
    private Rigidbody2D _rigid;
    private T _idleState;

    public override void Create()
    {

        _rigid = _transform.GetComponent<Rigidbody2D>();

    }

    protected override void OnExit()
    {

        _rigid.velocity = Vector3.zero;

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
        var dist = dir.sqrMagnitude;
        dir.Normalize();

        if(dist < _data.AttackAbleRange * _data.AttackAbleRange)
        {

            _rigid.velocity = Vector2.zero;
            return;

        }

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

            _controller.ChangeState(_idleState);

        }

    }

}



#endregion

#region Transition

public class EnemyTargetRangeTransition<T> : Transition<T> where T : System.Enum
{

    public EnemyTargetRangeTransition(Transform transform,float range, LayerMask targetLayer, T nextState, Func<bool> subCondition = null) : base(nextState)
    {

        _range = range;
        _targetLayer = targetLayer;
        _transform = transform;
        _subCondition = subCondition;

    }

    private Transform _transform;
    private float _range;
    private LayerMask _targetLayer;
    private Func<bool> _subCondition;

    public override bool ChackTransition()
    {

        if(_subCondition != null)
        {

            return _subCondition() && Physics2D.OverlapCircle(_transform.position, _range, _targetLayer);

        }

        return Physics2D.OverlapCircle(_transform.position, _range, _targetLayer);

    }

}
public class EnemyAttackTransition<T> : EnemyTargetRangeTransition<T> where T : System.Enum
{
    public EnemyAttackTransition(EnemyDataSO data, Transform transform, T nextState) : base(transform, data.AttackAbleRange, data.TargetAbleLayer, nextState)
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