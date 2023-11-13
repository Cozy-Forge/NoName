using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    private TetrisImg _selectBlock;             //현재 선택된 블록
    private WaitForSeconds _wfs;                 //MoveCo 코루틴 딜레이

    public int[,] board = new int[16,26];     //테트리스 보드 --> +3씩 예외처리

    public static int empty_place_size = 3;

    public TetrisImg[] test;

    private void Awake()
    {
        #region 싱글톤
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(transform);
            Debug.LogError($"{transform} : BlockManager is Multiply running!");
        }
        #endregion
    }

    private void Start()
    {
        _wfs = new WaitForSeconds(TetrisTileManager.Instance.speed);
        StartCoroutine(MoveCo());
    }

    //여긴 테스트용 나중에 지워야댐
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //CreateBlock(test[Random.Range(0,test.Length - 1)]);
            CreateBlock(test[0]);
        }

        if (Input.GetKeyDown(KeyCode.D))
            _selectBlock.DebugArr();

        MoveBlock();
    }

    //블록 이동 => 이넘으로 정리
    public void MoveBlock()
    {
        if(_selectBlock != null)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _selectBlock.Move(BLOCKMOVEDIR.LEFT);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                _selectBlock.Move(BLOCKMOVEDIR.RIGHT);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectBlock.RotateImage();
            }
        }
    }

    //블록 생성
    public void CreateBlock(TetrisImg tetrisImg)
    {
        _selectBlock = FAED.TakePool<TetrisImg>($"{tetrisImg.name}",new Vector3(0,0),Quaternion.identity,transform);
        _selectBlock.transform.localPosition = new Vector3(0, 0, 0);
        _selectBlock.Init();
    }

    //아래로 내려가게 하는 코루틴
    IEnumerator MoveCo()
    {
        while(true)
        {
            if (_selectBlock != null)
            {
                yield return _wfs;
                _selectBlock.Move(BLOCKMOVEDIR.DOWN);
            }
            else
                yield return null;
        }
    }

    /// <summary>
    /// 이미지가 이동할 수 있는지 확인 하는 함수
    /// </summary>
    /// <param name="pos">이동될 위치</param>
    /// <param name="fill">이미지의 채워진 픽셀</param>
    /// <returns>이동할 수 있으면 true 아니면 false를 리턴</returns>
    public bool CheckTile(XY pos, int[,] fill)
    {
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((fill[i,j] == 1 && board[pos.x + i + empty_place_size, pos.y + j + empty_place_size] == 1) 
                    || !CheckCanGo(pos))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 갈 수 있는 곳인지 확인
    /// </summary>
    /// <returns>갈 수 있으면 true, 아니면 false 리턴</returns>
    public bool CheckCanGo(XY pos)
    {
        if (pos.x < empty_place_size || pos.x >= 13 || pos.y < empty_place_size || pos.y >= 23 )
            return false;

        return true;
    }

    //넘쳤을때 지워버리는 코드
    public void EraseBoard(int cnt = 3, TetrisImg tetrisImg = null)
    {
        if(tetrisImg != null)
        {
            if (_selectBlock == tetrisImg)
                SetSelectBlockNull();

            FAED.InsertPool(tetrisImg.gameObject);            
        }

        for(int i = 0; i < cnt; i++)
        {
            //여기서 랜덤으로 팝해주셈 ㅇㅇ;
        }
    }

    public void SetSelectBlockNull() => _selectBlock = null;
}