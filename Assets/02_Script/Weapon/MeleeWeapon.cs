using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MeleeWeapon : Weapon
{
    Vector3 startLocalPosition;
    [SerializeField] private float _stingBackTime = 0.2f;

    protected override void DoAttack(Transform trm)
    {
        Debug.Log(transform.position);
        startLocalPosition = transform.localPosition;
        Debug.Log("근접공격");
        StartCoroutine(Sting(trm));
    }

    private IEnumerator Sting(Transform trm)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = trm.position;

        float elapsedTime = 0f;

        while (elapsedTime < _stingBackTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _stingBackTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        

        transform.localPosition = startLocalPosition;
    }


    private PlayerController _playerController;

    protected override void Awake()
    {
        base.Awake();
        _playerController = FindObjectOfType<PlayerController>();

    }

    public override void OnEquip()
    {
        _playerController.OnDashEvent += DashSting;
    }

   

    private void DashSting(Vector2 pos)
    {
        //돌진기
        transform.up = pos;
    }

    private void OnDestroy()
    {
        _playerController.OnDashEvent -= DashSting;
    }




}
