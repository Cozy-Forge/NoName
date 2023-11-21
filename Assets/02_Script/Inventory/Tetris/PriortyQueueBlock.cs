using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriortyQueueBlock : MonoBehaviour
{
    public static PriortyQueueBlock Instance = null;

    [HideInInspector] public List<TetrisImg> _tetrisImgList = new List<TetrisImg>(); // �켱���� ����Ʈ
    [HideInInspector] public bool isImgDestroy = false; //�г� �ȴ����� �ϴ� ����ó��
    
    private int size => _tetrisImgList.Count; // ����Ʈ ������

    private WaitForSeconds _destroyWfs = new WaitForSeconds(0.15f);
    private WaitForSeconds _downWfs = new WaitForSeconds(0.3f);

    PlayerWeaponContainer _weaponContainer;
    /// <summary>
    /// ������ �ν��Ͻ� ����ó��
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"PriortyQueueEquipment is multiply running!");
            Destroy(transform);
        }
        
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
        StartCoroutine(CoRandomPop(cnt));
    }

    public IEnumerator CoRandomPop(int cnt)
    {
        isImgDestroy = true;
        int tempIdx = 0;
        for (int i = 0; i < cnt; i++)
        {
            if (size > 0)
            {
                tempIdx = Random.Range(0, size);
                Pop(tempIdx);
                yield return _destroyWfs;
            }
        }
        yield return StartCoroutine(AllDownBlock());
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
    IEnumerator AllDownBlock()
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
                    yield return _downWfs;
                    _tetrisImgList[i].Move(BLOCKMOVEDIR.DOWN);
                    _tetrisImgList[i].SetPos();
                    isAllDown = false;
                }
            }
        }
        isImgDestroy = false;
        yield return null;
    }
}