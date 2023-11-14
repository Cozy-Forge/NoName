using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{

    private readonly int HASH_ISMOVE = Animator.StringToHash("IsMove");
    private readonly int HASH_DASHSTART = Animator.StringToHash("DashStart");
    private readonly int HASH_DASHEND = Animator.StringToHash("DashEnd");

    private Animator _animator;

    private void Awake()
    {
        
        _animator = GetComponent<Animator>();

    }

    public void SetIsMove(bool value)
    {

        _animator.SetBool(HASH_ISMOVE, value);

    }

    public void SetDash(bool value)
    {

        if (value)
        {

            _animator.SetTrigger(HASH_DASHSTART);

        }
        else
        {

            _animator.SetTrigger(HASH_DASHEND);

        }

    }

}
