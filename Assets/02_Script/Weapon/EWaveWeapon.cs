using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EWaveWeapon : Weapon
{
    private PlayerController _playerController;

    protected override void Awake()
    {
        base.Awake();
        _playerController = FindObjectOfType<PlayerController>();
    }

    

    private void SpawnWave()
    {
        var obj = Instantiate(gameObject, _playerController.transform.position, Quaternion.identity);
        _data.Range = transform.localScale.x;

        FAED.InvokeDelay(() =>
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }, 0.7f);

       StartCoroutine(ObjectTransformScaleOverTime(obj.transform, new Vector3(7f, 7f, 1f), 0.6f));
    }

    private IEnumerator ObjectTransformScaleOverTime(Transform objTransform, Vector3 targetScale, float duration)
    {
        float time = 0f;
        Vector3 initialScale = objTransform.localScale;

        while (time < duration)
        {
            objTransform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        objTransform.localScale = targetScale;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TestEnemyController enemyController = other.GetComponent<TestEnemyController>();
            if (enemyController != null)
            {
                //나중에 체력으로 바꾸기
                enemyController.Data.Speed -= 2;
            }
            else
            {
                Debug.Log("null");
            }
        }
    }*/

    
    protected override void DoAttack(Transform trm)
    {
        SpawnWave();
    }
}
