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

public class BigFroggyFireState : BigFroggyState
{
    public BigFroggyFireState(StateController<EnumBigFroggyState> controller, BigFroggyDataSO data) : base(controller, data)
    {
    }

    public enum FireType 
    { 
        
        T1,
        T2,
        T3,
        END
    
    }


    protected override void OnEnter()
    {

        FireType t = (FireType)Random.Range(0, (int)FireType.END);
        Fire(t);

    }

    protected override void Run()
    {
    }

    private void Fire(FireType type)
    {

        switch (type)
        {
            case FireType.T1:
                _controller.AddCoroutine(FireT1());
                break;
            case FireType.T2:
                _controller.AddCoroutine(FireT2());
                break;
            case FireType.T3:
                _controller.AddCoroutine(FireT3());
                break;
            case FireType.END:
                break;
        }

    }

    private IEnumerator FireT1()
    {

        for(int i = 0; i < 3; i++)
        {

            for(int j = 0; j < 15; j++)
            {

                var bullet = FAED.TakePool<Bullet>("FroggyBullet", _transform.position,
                    Quaternion.Euler(0, 0, (360 / 15) * j));
                bullet.Shoot();

            }

            yield return new WaitForSeconds(0.3f);

        }

        yield return new WaitForSeconds(0.5f);

        _controller.ChangeState(EnumBigFroggyState.Idle);
        _data.SetFireCoolDown();

    }

    private IEnumerator FireT2()
    {
        for (int i = 0; i < 10; i++)
        {

            for (int j = 0; j < 8; j++)
            {

                var bullet = FAED.TakePool<Bullet>("FroggyBullet", _transform.position,
                    Quaternion.Euler(0, 0, ((360 / 8) * j) +( i * 15)));
                bullet.Shoot();

            }

            yield return new WaitForSeconds(0.1f);

        }

        yield return new WaitForSeconds(0.5f);

        _controller.ChangeState(EnumBigFroggyState.Idle);
        _data.SetFireCoolDown();

    }

    private IEnumerator FireT3()
    {

        for (int i = 0; i < 36; i++)
        {

            var bullet = FAED.TakePool<Bullet>("FroggyBullet", _transform.position,
                Quaternion.Euler(0, 0, (360 / 36) * i));
            bullet.Shoot();

            yield return new WaitForSeconds(0.025f);

        }

        yield return new WaitForSeconds(0.5f);

        _controller.ChangeState(EnumBigFroggyState.Idle);
        _data.SetFireCoolDown();

    }

}