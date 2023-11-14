using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumPlayerState
{

    Move,
    Dash,
    Die

}

[RequireComponent(typeof(PlayerWeaponContainer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : StateController<EnumPlayerState>
{

    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private PlayerDataSO _data;

    public event Action<Vector2> OnDashEvent, OnMoveEvent;
    public event Action OnDashEndEvent;

    private void Awake()
    {

        _data = Instantiate(_data);

        var moveState = new MoveState(this, _inputReader, _data);
        var dashState = new DashState(this, _inputReader, _data);

        dashState.OnDashEvent += HandleOnDash;
        dashState.OnDashEndEvent += HandleOnDashEnd;

        moveState.OnMoveEvent += HandleOnMove;

        _stateContainer.Add(EnumPlayerState.Move, moveState);
        _stateContainer.Add(EnumPlayerState.Dash, dashState);

        CurrentState = EnumPlayerState.Move;

    }

    private void HandleOnMove(Vector2 value)
    {

        OnMoveEvent?.Invoke(value);

    }

    private void HandleOnDash(Vector2 value)
    {

        OnDashEvent?.Invoke(value);

    }

    private void HandleOnDashEnd()
    {

        OnDashEndEvent?.Invoke();

    }

}
