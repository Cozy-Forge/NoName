using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumPlayerState
{

    Move,
    Die

}

public class PlayerController : StateController<EnumPlayerState>
{

    [SerializeField] private PlayerInputReader _inputReader;    

}
