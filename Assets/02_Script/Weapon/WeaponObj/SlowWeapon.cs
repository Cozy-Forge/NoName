using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<IEnemyDataGetAble>(out var compo))
            {

                var data = compo.GetData();
                data.Speed = 2;
                Debug.Log("맞고 다 됨");
            }
            Debug.Log("맞기만함");
        }
        else
        {
            Debug.Log("안 맞았다 병신아");
        }
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
