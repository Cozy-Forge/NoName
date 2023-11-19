using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State<EnumPlayerState>
{

    public PlayerState(PlayerController controller, PlayerInputReader inputReader, PlayerDataSO data) : base(controller)
    {

        _inputReader = inputReader;
        _data = data;
        _animator = _transform.GetComponent<PlayerAnimator>();

    }

    protected PlayerInputReader _inputReader;
    protected PlayerDataSO _data;
    protected PlayerAnimator _animator;

}

public class PlayerFlipSubState : ISubState
{
    public PlayerFlipSubState(Transform trm, PlayerInputReader inputReader)
    {

        _spriteRenderer = trm.GetComponent<SpriteRenderer>();
        _inputReader = inputReader;

    }

    private SpriteRenderer _spriteRenderer;
    private PlayerInputReader _inputReader;

    public void Run()
    {

        _spriteRenderer.flipX = _inputReader.MoveInputDir.x switch
        {

            var x when x > 0 => true,
            var x when x < 0 => false,
            _ => _spriteRenderer.flipX

        };



    }

}

public class MoveState : PlayerState
{

    public event Action<Vector2> OnMoveEvent;

    public MoveState(PlayerController controller, PlayerInputReader inputReader, PlayerDataSO data) : base(controller, inputReader, data)
    {
    }

    private class AttackSubState : ISubState
    {

        public AttackSubState(PlayerWeaponContainer container, LayerMask targetLayer)
        {

            _container = container;
            _targetLayer = targetLayer;
            _transform = container.transform;
            

        }

        private Transform _transform;
        private PlayerWeaponContainer _container;
        private LayerMask _targetLayer;

        public void Run()
        {

            var hits = Physics2D.OverlapCircleAll(_transform.position, 100, _targetLayer);
            if(hits.Length != 0)
            {

                _container.CastingAll(hits);

            }

        }

    }

    private Rigidbody2D _rigid;

    public override void Create()
    {

        _rigid = _transform.GetComponent<Rigidbody2D>();

        var attackSub = new AttackSubState
            (_transform.GetComponent<PlayerWeaponContainer>(), _data.TargetLayer);

        var flipSub = new PlayerFlipSubState(_transform, _inputReader);

        _subStates.Add(attackSub);
        _subStates.Add(flipSub);

    }

    protected override void OnEnter()
    {

        _inputReader.OnDashKeyPressEvent += HandleDash;

    }

    protected override void OnExit()
    {

        _inputReader.OnDashKeyPressEvent -= HandleDash;

    }

    protected override void Run()
    {

        Move();

    }

    private void Move()
    {

        if (_isControlReleased) return;
        _rigid.velocity = _inputReader.MoveInputDir.normalized * _data.MoveSpeed;

        _animator.SetIsMove(_inputReader.MoveInputDir != Vector2.zero);

        OnMoveEvent?.Invoke(_inputReader.MoveInputDir);

    }

    private void HandleDash()
    {

        if (_data.IsCoolDown) return;

        _data.SetCoolDown();
        _controller.ChangeState(EnumPlayerState.Dash);

    }

}

public class DashState : PlayerState
{
    public DashState(PlayerController controller, PlayerInputReader inputReader, PlayerDataSO data) : base(controller, inputReader, data)
    {
    }

    private DashTransition _dashTransition;
    private Rigidbody2D _rigid;
    private Vector2 _dashEndPos;
    public event Action<Vector2> OnDashEvent;
    public event Action OnDashEndEvent;

    public class DashTransition : Transition<EnumPlayerState>
    {

        private Transform _transform;
        private Vector3 _endPos;
        private float _oldDest;

        public DashTransition(Transform transform, EnumPlayerState nextState) : base(nextState)
        {

            _transform = transform;

        }

        public void Set(Vector3 endPos)
        {

            _endPos = endPos;
            _oldDest = (_endPos - _transform.position).sqrMagnitude + 1;
        }

        public override bool ChackTransition()
        {

            var vec = _endPos - _transform.position;

            var dist = vec.sqrMagnitude;

            if (_oldDest - dist < 0)
            {

                return true;

            }
            _oldDest = dist;

            return false;

        }

    }

    public override void Create()
    {

        _dashTransition = new DashTransition(_transform, EnumPlayerState.Move);
        _transitions.Add(_dashTransition);
        _rigid = _transform.GetComponent<Rigidbody2D>();

    }

    protected override void OnEnter()
    {

        var dir = _inputReader.OldMoveInputDir;

        var hit = Physics2D.Raycast(_transform.position, dir, _data.DashLength, _data.DashObstacleLayer);

        if (hit.collider != null)
        {

            if(Vector2.Distance(hit.point, _transform.position) <= 0.5f)
            {

                OnDashEvent?.Invoke(dir);
                _dashEndPos = _transform.position;
                _controller.ChangeState(EnumPlayerState.Move);
                return;

            }

            _dashTransition.Set(hit.point - dir);
            _dashEndPos = hit.point - dir;


        }
        else
        {

            _dashTransition.Set((dir * _data.DashLength) + (Vector2)_transform.position);
            _dashEndPos = (dir * _data.DashLength) + (Vector2)_transform.position;

        }

        _rigid.velocity = dir * _data.DashPower;

        OnDashEvent?.Invoke(dir);
        _animator.SetDash(true);

    }

    protected override void OnExit()
    {

        _animator.SetDash(false);
        _transform.position = _dashEndPos;
        OnDashEndEvent?.Invoke();

    }

    protected override void Run()
    {



    }

}