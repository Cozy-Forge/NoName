using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private BulletDataSO _data;

    public BulletData Data;

    private void Awake()
    {
        
        Data = _data.CreateBulletData();

    }

    public void Shoot()
    {

        BulletJobManager.Instance.AddBullet(this);

    }

}
