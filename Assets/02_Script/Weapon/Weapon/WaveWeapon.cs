using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveWeapon : Weapon
{
    private PlayerController _playerController;
    [SerializeField] private GameObject _slowObj;

    protected override void Awake()
    {
        base.Awake();
        _playerController = FindObjectOfType<PlayerController>();
        
    }

    public override void OnEquip()
    {
        _playerController.OnDashEndEvent += SpawnWave;
    }

    private void SpawnWave()
    {
        var obj = Instantiate(_slowObj, _playerController.transform.position, Quaternion.identity);
        _data.Range = transform.localScale.x;


        StartCoroutine(ObjectTransformScaleOverTime(obj.transform, new Vector3(8f, 8f, 1f), 0.3f));
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

    


    private void OnDestroy()
    {
        _playerController.OnDashEndEvent -= SpawnWave;
    }


    protected override void DoAttack(Transform trm)
    {
        
    }
}
