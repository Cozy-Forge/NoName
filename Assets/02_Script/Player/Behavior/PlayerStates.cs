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

        private (Transform trm, float range) FirstObj(Collider2D[] arr)
        {

            Transform trm = null;
            float minRange = float.MaxValue;

            foreach(var item in arr)
            {

                float dist = Vector2.Distance(_transform.position, item.transform.position);

                if (dist < minRange)
                {

                    trm = item.transform;
                    minRange = dist;

                }

            }

            return (trm, minRange);

        }

        public void Run()
        {

            var hits = Physics2D.OverlapCircleAll(_transform.position, 100, _targetLayer);
            if(hits.Length != 0)
            {

                var obj = FirstObj(hits);
                _container.CastingAll(obj.trm, obj.range);

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

    public override void Run()
    {

        Move();

    }

    private void Move()
    {

        _rigid.velocity = _inputReader.MoveInputDir.normalized * _data.MoveSpeed;

    }

}
