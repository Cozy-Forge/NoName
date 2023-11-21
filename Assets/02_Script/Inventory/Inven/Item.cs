using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemSO itemData;
    [SerializeField] TetrisImg _tetrisImg;

    public ItemSO ItemData => itemData;
    public TetrisImg tetrisImg=>_tetrisImg;
}