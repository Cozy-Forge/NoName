using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State<EnumPlayerState>
{

    public PlayerState(PlayerController controller, PlayerInputReader inputReader) : base(controller)
    {

        _inputReader = inputReader;

    }

    protected PlayerInputReader _inputReader;

}

public class MoveState : PlayerState
{

    public MoveState(PlayerController controller, PlayerInputReader inputReader) : base(controller, inputReader)
    {
    }

    private Rigidbody2D _rigid;

    public override void Create()
    {

        _rigid = _transform.GetComponent<Rigidbody2D>();

    }

    public override void Run()
    {



    }

}
