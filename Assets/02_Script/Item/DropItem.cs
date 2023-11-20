using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    [SerializeField] private DropTableSO _dropTable;

    public void Drop()
    {

        var obj = _dropTable.GetRandomDropObjectInstance();

        var poss = GetAblePos();

        if(poss.Count > 0)
        {

            int idx = Random.Range(0, poss.Count);
            var point = poss[idx];

            var item = Instantiate(obj, transform.position, Quaternion.identity);
            item.transform.DOJump(point, 2f, 1, 0.4f).SetEase(Ease.OutSine);

        }

    }

    private List<Vector2> GetAblePos()
    {

        var list = new List<Vector2>();

        for(int x = -2; x <= 2; x++)
        {

            for (int y = -2; y <= 2; y++)
            {

                var point = transform.position + new Vector3(x, y);

                if(!Physics2D.OverlapCircle(point, 0.5f, LayerMask.GetMask("Wall")))
                {

                    list.Add(point);

                }

            }

        }

        return list;

    }

}
