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
    public event Action<Vector2> OnMoveEvent;

    private void OnEnable()
    {

        if (_input == null)
        {

            _input = new PlayerInputController();
            _input.Player.SetCallbacks(this);

        }

        _input.Enable();

    }

    public void OnKey(InputAction.CallbackContext context)
    {

        var value = context.ReadValue<Vector2>();
        MoveInputDir = value;
        OnMoveEvent?.Invoke(value);

    }

}
