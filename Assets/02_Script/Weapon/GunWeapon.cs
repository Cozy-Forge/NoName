using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{

    [SerializeField] private Transform _shootPos;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    protected override void Awake()
    {

        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

    }

    protected override void DoAttack(Transform trm)
    {

        var blt = FAED.TakePool<Bullet>("TestBullet", _shootPos.position, transform.rotation);
        blt.Shoot();

        FAED.TakePool<PoolingParticle>("ShootParticle", _shootPos.position, transform.rotation, transform);
        _audioSource.Play();

        transform.DOShakePosition(0.1f, 0.25f);

    }

    protected override void RotateWeapon(Transform target)
    {

        var dir = target.position - transform.position;
        dir.Normalize();
        dir.z = 0;

        _spriteRenderer.flipY = dir.x switch
        {

            var x when x > 0 => false,
            var x when x < 0 => true,
            _ => _spriteRenderer.flipY

        };

        transform.right = dir;

    }

}
