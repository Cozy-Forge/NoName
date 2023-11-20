using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HPObject
{

    [SerializeField] private float _notHitTime;

    public bool IsInvincibility { get; set; }

    private bool _isNotHit;

    public override void TakeDamage(float damage)
    {

        if (IsInvincibility || _isNotHit) return;

        _isNotHit = true;
        base.TakeDamage(damage);

        FAED.InvokeDelay(() =>
        {

            _isNotHit = false;

        }, _notHitTime);

    }

}
