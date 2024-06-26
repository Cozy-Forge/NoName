using System;
using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    Vector3 startLocalPosition;
    HPObject hpObject;
    [SerializeField] private float _stingBackTime = 0.2f;
    public bool _doAttack = true;

    protected override void DoAttack(Transform trm)
    {
        if (_doAttack)
        {
            startLocalPosition = transform.localPosition;
            StartCoroutine(Sting(trm));
        }
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
        _playerController.OnDashEndEvent += Doattack;
    }

    private void Doattack()
    {
        _doAttack = true;
    }

    private void DashSting(Vector2 pos)
    {
        _doAttack = false;
        //������
        transform.up = pos;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HPObject>().TakeDamage(10);
        }
    }

    private void OnDestroy()
    {
        _playerController.OnDashEndEvent -= Doattack;
        _playerController.OnDashEvent -= DashSting;
    }




}
