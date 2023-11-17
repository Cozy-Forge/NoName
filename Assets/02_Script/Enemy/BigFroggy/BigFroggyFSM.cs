using DG.Tweening;
using FD.Dev;
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
                .SetEase(Ease.InSine)
                .OnComplete(() => JumpEndEvent());

        }

    }

    protected override void Run()
    {



    }

    private void JumpEndEvent()
    {

        for(int i = 0; i <= _data.LandBulletCount; i++)
        {

            var bullet = FAED.TakePool<Bullet>("FroggyBullet", _transform.position, 
                Quaternion.Euler(0, 0, (360 / _data.LandBulletCount) * i));
            bullet.Shoot();

        }

        FAED.InvokeDelay(() =>
        {

            _data.SetJumpCoolDown();
            _controller.ChangeState(EnumBigFroggyState.Idle);

        }, 1f);

    }

}