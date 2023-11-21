using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriortyQueueBlock
{
    public static PriortyQueueBlock Instance = null;

    public List<TetrisImg> _tetrisImgList = new List<TetrisImg>(); // �켱���� ����Ʈ
    private int size => _tetrisImgList.Count; // ����Ʈ ������

    PlayerWeaponContainer _weaponContainer;
    /// <summary>
    /// ������ �ν��Ͻ� ����ó��
    /// </summary>
    public PriortyQueueBlock()
    {
        if (Instance != null)
            Debug.LogError($"PriortyQueueEquipment is multiply running!");
    }

    /// <summary>
    /// YPosition�� �������� �������� ����Ʈ�� �ִ´�.
    /// </summary>
    /// <param name="equipment">���� ������</param>
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
    /// �Ű����� cnt��ŭ ����Ʈ���� ���� ����.
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
    /// �Ű����� idx���� ����
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
    /// ����Ʈ ��ȯ, ��ȯ�� �� �ǵ�� �ȵ� ��¥ �ȵ� ��¥��¥ �ȵ�
    /// </summary>
    /// <returns>����Ʈ ����</returns>
    public List<TetrisImg> GetEquipmentList()
    {
        return _tetrisImgList;
    }

    /// <summary>
    /// ���ڸ��� ������ �ٽ� ������
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