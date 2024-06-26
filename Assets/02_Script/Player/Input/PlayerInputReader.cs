using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Player/Input")]
public class PlayerInputReader : ScriptableObject, PlayerInputController.IPlayerActions
{
    private PlayerInputController _input;

    public Vector2 MoveInputDir { get; private set; }
    public Vector2 OldMoveInputDir { get; private set; } = Vector2.left;

    public event Action<Vector2> OnMoveEvent;
    public event Action OnDashKeyPressEvent;
    public PlayerInputController InputData => _input;

    private void OnEnable()
    {

        if (_input == null)
        {

            _input = new PlayerInputController();
            _input.Player.SetCallbacks(this);

        }

        _input.Enable();

    }

    public void OnMove(InputAction.CallbackContext context)
    {

        var value = context.ReadValue<Vector2>();
        MoveInputDir = value;
        OnMoveEvent?.Invoke(value);

        if(value != Vector2.zero)
        {

            OldMoveInputDir = value;

        }

    }

    public void OnDash(InputAction.CallbackContext context)
    {


        if (context.performed)
        {

            OnDashKeyPressEvent?.Invoke();

        }

    }

    public void SetEnable(bool value)
    {
        if (value)
            _input.Enable();
        else
            _input.Disable();
    }
}
