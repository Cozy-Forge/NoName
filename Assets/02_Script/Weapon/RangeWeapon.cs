using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{

    [SerializeField] private string bulletPoolingKey;

    protected override void DoAttack(Transform trm)
    {

        var blt = FAED.TakePool<Bullet>(bulletPoolingKey, transform.position, Quaternion.identity);
        blt.transform.right = transform.up;

        BulletJobManager.Instance.AddBullet(blt);

    }

}
