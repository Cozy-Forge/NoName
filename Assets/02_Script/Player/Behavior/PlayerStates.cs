using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State<EnumPlayerState>
{

    public PlayerState(PlayerController controller, PlayerInputReader inputReader, PlayerDataSO data) : base(controller)
    {

        _inputReader = inputReader;
        _data = data;

    }

    protected PlayerInputReader _inputReader;
    protected PlayerDataSO _data;

}

public class MoveState : PlayerState
{

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

        _subStates.Add(attackSub);

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

    public class DashTransition : Transition<EnumPlayerState>
    {

        private Transform _transform;
        private Vector3 _endPos;
        private float _oldDest;

        public DashTransition(Transform transform,EnumPlayerState nextState) : base(nextState)
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

        var dir = _inputReader.MoveInputDir == Vector2.zero ? Vector2.right : _inputReader.MoveInputDir;

        var hit = Physics2D.Raycast(_transform.position, dir, _data.DashLength, _data.DashObstacleLayer);

        if (hit.collider != null)
        {

            _dashTransition.Set(hit.point);

        }
        else
        {

            _dashTransition.Set((dir * _data.DashLength) + (Vector2)_transform.position);

        }

        _rigid.velocity = dir * _data.DashPower;

    }

    protected override void Run()
    {



    }

}