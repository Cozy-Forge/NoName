using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if (PriortyQueueBlock.Instance == null)
            PriortyQueueBlock.Instance = new PriortyQueueBlock();
    }
}
