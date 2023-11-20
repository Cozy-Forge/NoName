using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using FD.Dev;
using System.Threading.Tasks;

public abstract class FroggyState : State<EnumBigFroggyState>
{
    protected FroggyState(StateController<EnumBigFroggyState> controller, BigFroggyDataSO data) : base(controller)
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

        if (hit != null)
        {

            _target = hit.transform;

        }
        else
        {

            _target = null;

        }

    }

}

public class FroggyJumpState : FroggyState
{
    public FroggyJumpState(StateController<EnumBigFroggyState> controller, BigFroggyDataSO data) : base(controller, data)
    {
    }

    private CinemachineImpulseSource _impulseSource;
    private SpriteRenderer _spriteRenderer;
    private Transform _shadowTrm;

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

    }

    private void HandleJumpStart()
    {

        SetTarget(_data.JumpRange);

        if (_target != null)
        {
            _spriteRenderer.flipX = _target.position.x > _transform.position.x;
            FAED.TakePool("FroggyJumpParticle", _transform.position + Vector3.down, Quaternion.identity);

            var poss = GetAblePos(_target);

            Vector3 pos;

            if (poss.Count > 0)
            {

                pos = poss[Random.Range(0, poss.Count)];

            }
            else
            {

                pos = _transform.position;

            }


            _transform.DOJump(pos, _data.JumpPower, 1, _data.JumpDuration)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {

                    JumpEndEvent();

                });

            _shadowTrm.DOMove(pos - new Vector3(0f, 0.2f, 0f), 1)
                .SetEase(Ease.InSine);

            _impulseSource.GenerateImpulse(0.3f);

        }
        else
        {

            _animater.SetJumpEnd();
            _data.SetJumpCoolDown();
            _controller.ChangeState(EnumBigFroggyState.Idle);

        }

    }

    private List<Vector2> GetAblePos(Transform target)
    {

        var list = new List<Vector2>();

        for (int x = -2; x <= 2; x++)
        {

            for (int y = -2; y <= 2; y++)
            {

                var point = target.position + new Vector3(x, y);

                if (!Physics2D.OverlapCircle(point, 0.5f, LayerMask.GetMask("Wall")))
                {

                    list.Add(point);

                }

            }

        }

        return list;

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
        _impulseSource.GenerateImpulse(0.5f);
        FAED.TakePool("FroggyJumpParticle", _transform.position + Vector3.down * 2, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        _data.SetJumpCoolDown();
        _controller.ChangeState(EnumBigFroggyState.Idle);

    }

}

public class FroggyFireState : FroggyState
{
    public FroggyFireState(StateController<EnumBigFroggyState> controller, BigFroggyDataSO data) : base(controller, data)
    {
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

        SetTarget(_data.FireRange);

        if(_target != null)
        {

            Fire();

        }
        else
        {

            _animater.SetFireEnd();
            _controller.ChangeState(EnumBigFroggyState.Idle);

        }


    }

    protected override void Run()
    {
    }

    private async void Fire()
    {
        
        var dir = _target.position - _transform.position;

        var blt = FAED.TakePool<Bullet>("FroggyBullet", _transform.position);
        blt.transform.right = dir.normalized;
        blt.Shoot();

        _animater.SetFireEnd();

        await Task.Delay(500);

        _controller.ChangeState(EnumBigFroggyState.Idle);
        _data.SetFireCoolDown();

    }

}