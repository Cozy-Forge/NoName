using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{

    [SerializeField] private Transform _shootPos;
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {

        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected override void DoAttack(Transform trm)
    {

        var blt = FAED.TakePool<Bullet>("TestBullet", _shootPos.position, transform.rotation);
        BulletJobManager.Instance.AddBullet(blt);

       FAED.TakePool<PoolingParticle>("ShootParticle", _shootPos.position, transform.rotation, transform);

        transform.DOShakePosition(0.1f, 0.25f);

    }

    protected override void RotateWeapon(Transform target)
    {

        var dir = target.position - transform.position;
        dir.Normalize();

        _spriteRenderer.flipY = dir.x switch
        {

            var x when x > 0 => false,
            var x when x < 0 => true,
            _ => _spriteRenderer.flipY

        };

        transform.right = dir;

    }

}
