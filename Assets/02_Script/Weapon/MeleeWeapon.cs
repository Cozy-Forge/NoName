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
        Vector3 startLocalPosition = transform.localPosition;
        Vector3 backwardPosition = startLocalPosition - new Vector3(0, 0, 1); // Move backward in local z-axis
        Vector3 forwardPosition = startLocalPosition + new Vector3(0, 0, 1);  // Move forward in local z-axis

        float t = 0.0f;

        // Move backward
        while (t < 1.0f)
        {
            t += Time.deltaTime / (_swingTime * 0.5f); // Move backward for half of the swing time
            transform.localPosition = Vector3.Lerp(startLocalPosition, backwardPosition, t);
            yield return null;
        }

        // Wait for the swing delay
        yield return new WaitForSeconds(_swingDelayTime);

        t = 0.0f;

        // Move forward
        while (t < 1.0f)
        {
            t += Time.deltaTime / (_swingTime * 0.5f); // Move forward for the remaining half of the swing time
            transform.localPosition = Vector3.Lerp(backwardPosition, forwardPosition, t);
            yield return null;
        }

        // Reset to the starting position
        transform.localPosition = startLocalPosition;

        // If you want to stop the coroutine after the swing is complete, you can remove the following line:
        // StopCoroutine(Swing());
    }




}
