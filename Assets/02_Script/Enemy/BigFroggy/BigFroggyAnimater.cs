using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFroggyAnimater : MonoBehaviour
{

    private readonly int HASN_FIRE = Animator.StringToHash("Fire");
    private readonly int HASN_FIREEND = Animator.StringToHash("FireEnd");
    private readonly int HASN_JUMP = Animator.StringToHash("Jump");
    private readonly int HASN_JUMPEND = Animator.StringToHash("JumpEnd");
    private readonly int HASN_JUMPSTART = Animator.StringToHash("JumpStart");

    public event Action OnJumpStartEvent;
    public event Action OnFireStartEvent;

    private Animator _animator;

    private void Awake()
    {
        
        _animator = GetComponent<Animator>();

    }

    public void SetFire()
    {

        _animator.SetTrigger(HASN_FIRE);

    }

    public void SetFireEnd()
    {

        _animator.SetTrigger(HASN_FIREEND);

    }

    public void SetJump() 
    {

        _animator.SetTrigger(HASN_JUMP);
    
    }

    public void SetJumpEnd()
    {

        _animator.SetTrigger(HASN_JUMPEND);

    }

    public void OnFireStart()
    {

        OnFireStartEvent?.Invoke();

    }

    public void OnJumpStart()
    {

        _animator.SetTrigger(HASN_JUMPSTART);
        OnJumpStartEvent?.Invoke();

    }

}
