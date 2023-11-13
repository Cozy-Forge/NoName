using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    private TetrisImg _selectBlock;             //���� ���õ� ���
    private WaitForSeconds _wfs;                 //MoveCo �ڷ�ƾ ������

    public int[,] board = new int[16,26];     //��Ʈ���� ���� --> +3�� ����ó��

    public static int empty_place_size = 3;

    public TetrisImg[] test;

    private void Awake()
    {
        #region �̱���
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

    //���� �׽�Ʈ�� ���߿� �����ߴ�
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

    //��� �̵� => �̳����� ����
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

    //��� ����
    public void CreateBlock(TetrisImg tetrisImg)
    {
        _selectBlock = FAED.TakePool<TetrisImg>($"{tetrisImg.name}",new Vector3(0,0),Quaternion.identity,transform);
        _selectBlock.transform.localPosition = new Vector3(0, 0, 0);
        _selectBlock.Init();
    }

    //�Ʒ��� �������� �ϴ� �ڷ�ƾ
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
    /// �̹����� �̵��� �� �ִ��� Ȯ�� �ϴ� �Լ�
    /// </summary>
    /// <param name="pos">�̵��� ��ġ</param>
    /// <param name="fill">�̹����� ä���� �ȼ�</param>
    /// <returns>�̵��� �� ������ true �ƴϸ� false�� ����</returns>
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
    /// �� �� �ִ� ������ Ȯ��
    /// </summary>
    /// <returns>�� �� ������ true, �ƴϸ� false ����</returns>
    public bool CheckCanGo(XY pos)
    {
        if (pos.x < empty_place_size || pos.x >= 13 || pos.y < empty_place_size || pos.y >= 23 )
            return false;

        return true;
    }

    //�������� ���������� �ڵ�
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
            //���⼭ �������� �����ּ� ����;
        }
    }

    public void SetSelectBlockNull() => _selectBlock = null;
}