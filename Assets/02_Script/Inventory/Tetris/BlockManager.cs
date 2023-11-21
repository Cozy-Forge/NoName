using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    private TetrisImg _selectBlock;             //���� ���õ� ���
    public TetrisImg selectBlock=>_selectBlock;
    private WaitForSeconds _wfs;                 //MoveCo �ڷ�ƾ ������

    public int[,] board = new int[26, 26];     //��Ʈ���� ���� --> +3�� ����ó��

    public static int empty_place_size = 3;

    private void Awake()
    {
        #region �̱���
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(transform);
            Debug.LogError($"{transform} : BlockManager is Multiple running!");
        }
        #endregion
    }

    private void Start()
    {
        _wfs = new WaitForSeconds(TetrisTileManager.Instance.speed);
    }

    private void OnEnable()
    {
        StartCoroutine(MoveCo());
    }

    private void Update()
    {
        MoveBlock();
    }

    //��� �̵� => �̳����� ����
    public void MoveBlock()
    {
        if (_selectBlock != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _selectBlock.Move(BLOCKMOVEDIR.LEFT);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _selectBlock.Move(BLOCKMOVEDIR.RIGHT);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectBlock.RotateImage();
            }
        }
    }

    //��� ����
    public void CreateBlock(TetrisImg tetrisImg)
    {
        _selectBlock = FAED.TakePool<TetrisImg>($"{tetrisImg.name}", new Vector3(0, 0), Quaternion.identity, transform);
        _selectBlock.Init();
    }

    //�Ʒ��� �������� �ϴ� �ڷ�ƾ
    IEnumerator MoveCo()
    {
        while (true)
        {
            if (_selectBlock != null)
            {
                yield return _wfs;
                if (_selectBlock != null)
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
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((fill[i, j] == 1 && board[pos.x + i + empty_place_size, pos.y + j + empty_place_size] == 1)
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
        if (pos.x < empty_place_size || pos.x >= 13 || pos.y < empty_place_size || pos.y >= 23)
            return false;

        return true;
    }

    //�������� ���������� �ڵ�
    public void EraseBoard(TetrisImg tetrisImg = null, int cnt = 3)
    {
        if (tetrisImg != null)
        {
            if (_selectBlock == tetrisImg)
                SetSelectBlockNull();

            tetrisImg.ClearBoard();
            FAED.InsertPool(tetrisImg.gameObject);
        }

        PriortyQueueBlock.Instance.RandomPop(1);
    }

    public void SetSelectBlockNull() => _selectBlock = null;

    public void DebugBoard()
    {
        string s = "";
        for (int i = 3; i < TetrisTileManager.Instance.boardYSize + 3; i++)
        {
            for (int j = 3  ; j < TetrisTileManager.Instance.boardXSize + 3; j++)
            {
                s += board[i, j];
            }
            s += '\n';
        }
        Debug.Log(s);
    }
}