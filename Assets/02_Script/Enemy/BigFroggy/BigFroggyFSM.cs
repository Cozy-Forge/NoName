using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BigFroggyState : State<EnumBigFroggyState>
{
    protected BigFroggyState(StateController<EnumBigFroggyState> controller, BigFroggyDataSO data) : base(controller)
    {

        _data = data;

    }

    protected BigFroggyDataSO _data;
    protected Transform _target;

    protected void SetTarget(float lenght)
    {

        var hit = Physics2D.OverlapCircle(_transform.position, lenght, _data.TargetAbleLayer);

        if(hit != null)
        {

            _target = hit.transform;

        }
        else
        {

            _target = null;

        }

    }

}

public class BigFroggyJumpState : BigFroggyState
{
    public BigFroggyJumpState(StateController<EnumBigFroggyState> controller, BigFroggyDataSO data) : base(controller, data)
    {
    }

    protected override void OnEnter()
    {

        SetTarget(_data.JumpRange);

        if(_target != null)
        {

            _transform.DOJump(_target.position, _data.JumpPower, 1, _data.JumpDuration)
                .OnComplete(() => JumpEndEvent());

        }

    }

    protected override void Run()
    {



    }

    private void JumpEndEvent()
    {



    }

}