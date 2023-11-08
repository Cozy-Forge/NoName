using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumPlayerState
{

    Move,
    Die

}

[RequireComponent(typeof(PlayerWeaponContainer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : StateController<EnumPlayerState>
{

    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private PlayerDataSO _data;

    private void Awake()
    {

        var moveState = new MoveState(this, _inputReader, _data);

        _stateContainer.Add(EnumPlayerState.Move, moveState);

    }

}
