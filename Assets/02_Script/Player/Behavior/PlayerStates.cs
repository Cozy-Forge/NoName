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

        public AttackSubState()
        {



        }

        public void Run()
        {



        }

    }

    private Rigidbody2D _rigid;

    public override void Create()
    {

        _rigid = _transform.GetComponent<Rigidbody2D>();

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
