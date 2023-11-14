using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MyClass
{
    public Vector2 position;
    public Item resultItem;
}

public class ItemBoard : MonoBehaviour
{
    [SerializeField] private List<MyClass> board = new List<MyClass>();



    public Item GetNearItem(Vector2 pos)
    {
        int idx = 0;
        float minDistance = float.MaxValue;
        for (int i = 0; i < board.Count; ++i)
        {
            float currentDistance = Vector2.Distance(pos, board[i].position);
            if (currentDistance < minDistance)
            {
                idx = i;
                minDistance = currentDistance;
            }
        }
        return board[idx].resultItem;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var item in board)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(item.position, 0.5f);
        }
    }
}
