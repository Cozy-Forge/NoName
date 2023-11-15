using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PriortyQueueBlock 
{
    public static PriortyQueueBlock Instance = null;

    public List<TetrisImg> _tetrisImgList = new List<TetrisImg>(); // 우선순위 리스트
    private int size => _tetrisImgList.Count; // 리스트 사이즈

    /// <summary>
    /// 생성자 인스턴스 예외처리
    /// </summary>
    public PriortyQueueBlock()
    {
        if (Instance != null)
            Debug.LogError($"PriortyQueueEquipment is multiply running!");
    }

    /// <summary>
    /// YPosition을 기준으로 아이템을 리스트에 넣는다.
    /// </summary>
    /// <param name="equipment">넣을 아이템</param>
    public void Push(TetrisImg tetrisImg)
    {
        for (int i = 0; i < size; i++)
        {
            if (tetrisImg.pos.y < _tetrisImgList[i].pos.y)
            {
                _tetrisImgList.Insert(i, tetrisImg);
                return;
            }
        }
        _tetrisImgList.Add(tetrisImg);
    }

    /// <summary>
    /// 매개변수 cnt만큼 리스트에서 값을 뺀다.
    /// </summary>
    /// <param name="cnt"></param>
    public void RandomPop(int cnt)
    {
        int tempIdx;
        for (int i = 0; i < cnt; i++)
        {
            tempIdx = Random.Range(0, size - 1);
            Pop(tempIdx);
        }
    }

    /// <summary>
    /// 매개변수 idx값이 빠짐
    /// </summary>
    /// <param name="idx"></param>
    public void Pop(int idx)
    {
        FAED.InsertPool(_tetrisImgList[idx].gameObject);
        _tetrisImgList.RemoveAt(idx);
    }

    /// <summary>
    /// 리스트 반환, 반환된 애 건들면 안됨 진짜 안됨 진짜진짜 안됨
    /// </summary>
    /// <returns>리스트 리턴</returns>
    public List<TetrisImg> GetEquipmentList()
    {
        return _tetrisImgList;
    }

    /// <summary>
    /// 빈자리가 있으면 다시 내려감
    /// </summary>
    public void AllDownBlock()
    {
        List<TetrisImg> temptetrisImgList = new List<TetrisImg>(); // 템프에 옮기고
        while(size > 0)
        {
            temptetrisImgList.Add(_tetrisImgList[0]);
            Pop(0);
        }

    }
}