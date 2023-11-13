using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{

    private readonly int HASH_ISMOVE = Animator.StringToHash("IsMove");

    private Animator _animator;

    private void Awake()
    {
        
        _animator = GetComponent<Animator>();

    }

    public void SetIsMove(bool value)
    {

        _animator.SetBool(HASH_ISMOVE, value);

    }

}
