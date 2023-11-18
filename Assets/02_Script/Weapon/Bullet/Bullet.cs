using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private BulletDataSO _data;
    [SerializeField] private string _bulletDestroyParticle;

    private bool _isAdd;

    public BulletData Data;

    private void Awake()
    {
        
        Data = _data.CreateBulletData();

    }

    public void Shoot()
    {

        BulletJobManager.Instance.AddBullet(this);
        _isAdd = true;
        StartCoroutine(ReleaseBullet());

    }

    public void Release()
    {

        if (BulletJobManager.Instance.RemoveBullet(this))
        {

            FAED.InsertPool(gameObject);

        }
        else
        {

            Destroy(gameObject);

        }

        _isAdd = false;

        StopAllCoroutines();

    }

    private IEnumerator ReleaseBullet()
    {

        yield return new WaitForSeconds(20f);
        Release();

    }

    protected virtual void HitOther() 
    {

        if (_bulletDestroyParticle == string.Empty) return;
        FAED.TakePool(_bulletDestroyParticle, transform.position, Quaternion.identity);
    
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (!_isAdd) return;

        foreach(var item in Data.HitAbleTag)
        {

            if (collision.CompareTag(item))
            {

                if(collision.TryGetComponent<HPObject>(out var hp))
                {

                    hp.TakeDamage(Data.Damage);

                }
                else if(collision.TryGetComponent<Hitbox>(out var box))
                {

                    box.Casting(Data.Damage);

                }

                HitOther();
                Release();

            }

        }

    }

}
