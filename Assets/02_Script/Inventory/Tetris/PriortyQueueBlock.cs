using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriortyQueueBlock
{
    public static PriortyQueueBlock Instance = null;

    public List<TetrisImg> _tetrisImgList = new List<TetrisImg>(); // 우선순위 리스트
    private int size => _tetrisImgList.Count; // 리스트 사이즈

    PlayerWeaponContainer _weaponContainer;
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
    public void RandomPop(int cnt = 3)
    {
        int tempIdx = 0;
        for (int i = 0; i < cnt; i++)
        {
            if (size > 0)
            {
                tempIdx = Random.Range(0, size);
                Pop(tempIdx);
            }
        }
        AllDownBlock();
    }

    /// <summary>
    /// 매개변수 idx값이 빠짐
    /// </summary>
    /// <param name="idx"></param>
    public void Pop(int idx)
    {
        if(_weaponContainer == null)
            _weaponContainer = GameObject.Find("Player").GetComponent<PlayerWeaponContainer>();
        _weaponContainer.RemoveWeapon(_tetrisImgList[idx].weapon);
        _tetrisImgList[idx].ClearBoard();
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
        bool isAllDown = false;

        while (!isAllDown)
        {
            isAllDown = true;
            for (int i = 0; i < size; i++)
            {
                _tetrisImgList[i].ClearBoard();
                while (_tetrisImgList[i].CanGo(BLOCKMOVEDIR.DOWN, false))
                {
                    if (!_tetrisImgList[i].CollisionOtherBlock(BLOCKMOVEDIR.DOWN))
                    {
                        _tetrisImgList[i].FillBoard(false);
                        break;
                    }
                    _tetrisImgList[i].Move(BLOCKMOVEDIR.DOWN);
                    _tetrisImgList[i].SetPos();
                    isAllDown = false;
                }
            }
        }
    }
}