using FD.Dev;
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

        StartCoroutine(ReleaseBullet());

    }

    public void Release()
    {

        BulletJobManager.Instance.RemoveBullet(this);
        FAED.InsertPool(gameObject);

    }

    private IEnumerator ReleaseBullet()
    {

        yield return new WaitForSeconds(20f);
        Release();

    }

    protected virtual void HitOther() { }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        foreach(var item in Data.HitAbleTag)
        {

            if (collision.CompareTag(item))
            {

                if(collision.TryGetComponent<HPObject>(out var hp))
                {

                    hp.TakeDamage(Data.Damage);

                }

                if(collision.TryGetComponent<FeedbackPlayer>(out var feedback))
                {

                    feedback.PlayFeedback(_data.Damage);

                }

                HitOther();
                Release();

            }

        }

    }

}
