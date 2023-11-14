using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/UI/Input")]
public class PlayerUIReader : ScriptableObject, PlayerUIController.IUIActions
{
    private PlayerUIController _input;

    public enum InputUIBtnDir
    {
        Left, Up, Right, Down
    }

    public event Action<InputUIBtnDir> OnKeyDown;
    public event Action<InputUIBtnDir> OnKeyUp;
    public event Action OnUISelect;

    private void OnEnable()
    {

        if (_input == null)
        {

            _input = new PlayerUIController();
            _input.UI.SetCallbacks(this);

        }

        _input.Enable();

    }

    public void SetEnable(bool value)
    {
        if (value)
            _input.Enable();
        else
            _input.Disable();
    }


    public void OnDown(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnKeyDown?.Invoke(InputUIBtnDir.Down);
        }
        else if(context.canceled)
        {
            OnKeyUp?.Invoke(InputUIBtnDir.Down);
        }

    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnKeyDown?.Invoke(InputUIBtnDir.Left);
        }
        else if (context.canceled)
        {
            OnKeyUp?.Invoke(InputUIBtnDir.Left);
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnKeyDown?.Invoke(InputUIBtnDir.Right);
        }
        else if (context.canceled)
        {
            OnKeyUp?.Invoke(InputUIBtnDir.Right);
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnKeyDown?.Invoke(InputUIBtnDir.Up);
        }
        else if (context.canceled)
        {
            OnKeyUp?.Invoke(InputUIBtnDir.Up);
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
            OnUISelect?.Invoke();
    }
}
