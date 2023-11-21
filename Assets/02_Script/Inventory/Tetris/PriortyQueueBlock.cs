using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriortyQueueBlock : MonoBehaviour
{
    public static PriortyQueueBlock Instance = null;

    [HideInInspector] public List<TetrisImg> _tetrisImgList = new List<TetrisImg>(); // 우선순위 리스트
    [HideInInspector] public bool isImgDestroy = false; //패널 안닫히게 하는 예외처리
    
    private int size => _tetrisImgList.Count; // 리스트 사이즈

    private WaitForSeconds _destroyWfs = new WaitForSeconds(0.15f);
    private WaitForSeconds _downWfs = new WaitForSeconds(0.3f);

    PlayerWeaponContainer _weaponContainer;
    /// <summary>
    /// 생성자 인스턴스 예외처리
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