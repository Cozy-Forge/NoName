using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{

    [SerializeField] protected float _maxHP;

    protected float _currentHP;
    protected bool _isDie;

    public event Action<float> OnTakeDamageEvent, OnHealEvent;
    public event Action OnDieEvent;

    protected virtual void Awake()
    {

        _currentHP = _maxHP;

    }

    public virtual void TakeDamage(float damage)
    {

        if (_isDie) return;

        _currentHP -= damage;

        _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);

        if(_currentHP <= 0)
        {

            _isDie = true;
            OnDieEvent?.Invoke();

        }

        OnTakeDamageEvent?.Invoke(damage);

    }

    public virtual void HealDamage(float value)
    {

        if (_isDie) return;

        _currentHP += value;

        _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);

        OnHealEvent?.Invoke(value);

    }

}
