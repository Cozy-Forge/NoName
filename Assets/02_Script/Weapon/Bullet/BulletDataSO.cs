using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletData
{

    public float Speed;
    public float Damage;

}

[CreateAssetMenu(menuName = "SO/Bullet/Data")]
public class BulletDataSO : ScriptableObject
{

    public float Speed;
    public float Damage;

    public BulletData CreateBulletData()
    {

        return new BulletData
        {

            Speed = Speed,
            Damage = Damage

        };

    }

}
