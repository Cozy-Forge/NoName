using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField] protected WeaponDataSO _data;

    protected bool _isCoolDown { get; private set; }

    public void CastingWeapon(Transform target, float range)
    {

        if (_data.Range < range) return;

        RotateWeapon(target);

        if (_isCoolDown) return;

        _isCoolDown = true;

        FAED.InvokeDelay(() =>
        {

            _isCoolDown = false;

        }, _data.AttackCoolDown);

        DoAttack();

    }

    protected virtual void RotateWeapon(Transform target)
    {

        var dir = target.position - transform.position;

        transform.up = dir.normalized;

    }

    protected abstract void DoAttack();

}