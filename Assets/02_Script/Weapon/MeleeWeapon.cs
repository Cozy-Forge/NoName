using System;
using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float _swingTime = 5.0f;
    public float _swingDelayTime = 0.3f;

    protected override void DoAttack(Transform trm)
    {
        Debug.Log("근접공격");

        StartCoroutine(Swing());
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
        transform.LookAt(pos);
    }

    private IEnumerator Swing()
    {
        Vector3 startLocalPosition = transform.localPosition;
        Vector3 backwardPosition = startLocalPosition - new Vector3(0, 0, 3); 
        Vector3 forwardPosition = startLocalPosition + new Vector3(0, 0, 1);  

        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / (_swingTime * 0.5f);
            transform.localPosition = Vector3.Lerp(startLocalPosition, backwardPosition, t);
            yield return null;
        }

        yield return new WaitForSeconds(_swingDelayTime);

        t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / (_swingTime * 0.5f); 
            transform.localPosition = Vector3.Lerp(backwardPosition, forwardPosition, t);
            yield return null;
        }

        transform.localPosition = startLocalPosition;

         StopCoroutine(Swing());
    }


}
