using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PriortyQueueBlock 
{
    public static PriortyQueueBlock Instance = null;

    public List<TetrisImg> _tetrisImgList = new List<TetrisImg>(); // �켱���� ����Ʈ
    private int size => _tetrisImgList.Count; // ����Ʈ ������

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
    /// �Ű����� idx���� ����
    /// </summary>
    /// <param name="idx"></param>
    public void Pop(int idx)
    {
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
        List<TetrisImg> temptetrisImgList = new List<TetrisImg>(); // ������ �ű��
        while(size > 0)
        {
            temptetrisImgList.Add(_tetrisImgList[0]);
            Pop(0);
        }

    }
}