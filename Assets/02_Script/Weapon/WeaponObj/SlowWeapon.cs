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
                Debug.Log("�°� �� ��");
            }
            Debug.Log("�±⸸��");
        }
        else
        {
            Debug.Log("�� �¾Ҵ� ���ž�");
        }
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
