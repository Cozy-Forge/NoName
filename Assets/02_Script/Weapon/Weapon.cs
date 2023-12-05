using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField] protected WeaponDataSO _so;
    protected WeaponData _data;

    protected bool _isCoolDown { get; private set; }

    protected virtual void Awake()
    {

        _data = _so.Data;

    }

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

        DoAttack(target);

    }

    protected virtual void RotateWeapon(Transform target)
    {

        var dir = target.position - transform.position;

        transform.up = dir.normalized;

    }

    protected abstract void DoAttack(Transform trm);
    public virtual void OnEquip() { }

}
