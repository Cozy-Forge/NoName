using System;
using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{

    public float _swingTime = 0.2f;
    public float _swingDelayTime = 1f;
  
    protected override void DoAttack(Transform trm)
    {
        Debug.Log("근접공격");

        StartCoroutine(Swing());
    }

    private IEnumerator Swing()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation1 = Quaternion.Euler(0, 0, 40) * startRotation;

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / _swingTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation1, t);
            yield return null;
        }

        yield return new WaitForSeconds(_swingDelayTime);

        StopCoroutine(Swing());
    }

}
