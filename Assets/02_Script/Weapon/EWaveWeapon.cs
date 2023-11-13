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

        while (time < duration)
        {
            objTransform.localScale = Vector3.Scale(targetScale, new Vector3(time / duration, time / duration, 1f));
            time += Time.deltaTime;
            yield return null;
        }

        objTransform.localScale = targetScale;
    }

    protected override void DoAttack(Transform trm)
    {
        SpawnWave();
    }
}
