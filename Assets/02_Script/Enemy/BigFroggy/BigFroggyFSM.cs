using Cinemachine;
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
        _animater = _transform.GetComponent<BigFroggyAnimater>();

    }

    protected BigFroggyDataSO _data;
    protected Transform _target;
    protected BigFroggyAnimater _animater;

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

    private CinemachineImpulseSource _impulseSource;
    private SpriteRenderer _spriteRenderer;
    private Transform _shadowTrm;
    private Rect _rect;

    public override void Create()
    {

        _animater.OnJumpStartEvent += HandleJumpStart;
        _impulseSource = _transform.GetComponent<CinemachineImpulseSource>();
        _spriteRenderer = _transform.GetComponent<SpriteRenderer>();
        _shadowTrm = _transform.Find("Shadow");

    }

    protected override void OnEnter()
    {

        _animater.SetJump();

        Debug.Log(_transform.parent);
        Debug.Log(_transform.parent.position);

        _rect = new Rect(
            _transform.parent.position.x - (50 / 2),
            _transform.parent.position.y - (50 / 2),
            50, 50); //나중에 사이즈로 바꿔
    }

    private void HandleJumpStart()
    {

        SetTarget(_data.JumpRange);

        if (_target != null)
        {

            var dir = _target.position;

            if (!_rect.Contains(dir))
            {

                dir = new Vector3(
                    Mathf.Clamp(dir.x, _rect.xMin, _rect.xMax),
                    Mathf.Clamp(dir.y, _rect.yMin, _rect.yMax));

            }

            _spriteRenderer.flipX = dir.x > _transform.position.x;
            FAED.TakePool("BigFroggyJumpParticle", dir + Vector3.down * 2, Quaternion.identity);
            var obj = FAED.TakePool("BigFroggyWarning", dir + Vector3.down * 2, Quaternion.identity);

            _transform.DOJump(dir, _data.JumpPower, 1, _data.JumpDuration)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {

                    JumpEndEvent();
                    FAED.InsertPool(obj);

                });

            _shadowTrm.DOMove(dir - new Vector3(0f, 1.5f, 0f), 1)
                .SetEase(Ease.InSine);

            _impulseSource.GenerateImpulse(0.7f);

        }
        else
        {

            _animater.SetJumpEnd();
            _data.SetJumpCoolDown();
            _controller.ChangeState(EnumBigFroggyState.Idle);

        }

    }

    protected override void Run()
    {



    }

    private void JumpEndEvent()
    {

        _controller.AddCoroutine(LandBltCo());

    }

    private IEnumerator LandBltCo()
    {


        _animater.SetJumpEnd();
        _impulseSource.GenerateImpulse(1.5f);
        FAED.TakePool("BigFroggyJumpParticle", _transform.position + Vector3.down * 2, Quaternion.identity);

        for(int i = 0; i < 3; i++)
        {

            for (int j = 0; j <= _data.LandBulletCount; j++)
            {

                var bullet = FAED.TakePool<Bullet>("FroggyBullet", _transform.position,
                    Quaternion.Euler(0, 0, ((360 / _data.LandBulletCount) * i) + j * 10));
                bullet.Shoot();

            }

            yield return new WaitForSeconds(0.1f);

        }

        yield return new WaitForSeconds(0.5f);

        _data.SetJumpCoolDown();
        _controller.ChangeState(EnumBigFroggyState.Idle);

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

    public override void Create()
    {

        _animater.OnFireStartEvent += HandleFireStart;

    }

    protected override void OnEnter()
    {

        _animater.SetFire();

    }

    private void HandleFireStart()
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

        _animater.SetFireEnd();

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
                    Quaternion.Euler(0, 0, ((360 / 8) * j) + (i * 15)));
                bullet.Shoot();

            }

            yield return new WaitForSeconds(0.1f);

        }

        _animater.SetFireEnd();

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

        _animater.SetFireEnd();

        yield return new WaitForSeconds(0.5f);

        _controller.ChangeState(EnumBigFroggyState.Idle);
        _data.SetFireCoolDown();

    }

}