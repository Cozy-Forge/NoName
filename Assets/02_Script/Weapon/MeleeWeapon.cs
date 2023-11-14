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
        
        transform.up = trm.position;
        transform.position = trm.position;
        yield return new WaitForSeconds(_stingBackTime);
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
        //돌진긴
        transform.up = pos;

    }

    private void OnDestroy()
    {
        _playerController.OnDashEvent -= DashSting;
    }




}
