using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public enum BLOCKMOVEDIR
{
    DOWN = 0,
    UP = 1,
    LEFT = 2,
    RIGHT = 3
}

public struct XY
{
    public int x;
    public int y;
}

public class TetrisImg : MonoBehaviour
{
    private Texture2D _texture;
    private RectTransform _rectTransform;

    private int[,] _board = new int[4, 4];
    public int[,] board => _board;

    private XY _pos = new XY();
    public XY pos => _pos;

    private int[,] _tempboard = new int[4, 4];

    const int _row = 4;
    const int _col = 4;
    const int _pixerSize = 16;
    const int _spriteSize = 200;
    const int _tileLength = 50;

    public bool _isDebug = false;

    private Color[] _tempPixels;

    private void Awake()
    {
        Debug.Log($"test/{transform.name}");
        transform.name = transform.name.Replace("(Clone)", "");

        _texture = Resources.Load<Texture2D>($"test/{transform.name}") as Texture2D;
        _rectTransform = GetComponent<RectTransform>();

        #region 예외처리

        if (_texture == null)
            Debug.LogError($"{transform} : _texture path is wrong!");

        if (!_texture.isReadable)
            Debug.LogError($"{transform} : This sprite can't read!");

        if (_rectTransform.rect.width != _spriteSize || _rectTransform.rect.height != _spriteSize)
        {
            Debug.LogWarning($"{transform} : This sprite's size is not 200!");
            _rectTransform.sizeDelta = new Vector2(_spriteSize, _spriteSize);
        }
        #endregion

        CheckImage();

        #region 디버그용
        if (_isDebug)
        {
            string s = "";
            for (int i = _row - 1; i >= 0; i--)
            {
                for (int j = 0; j < _col; j++)
                {
                    s += _board[i, j].ToString() + " ";
                }
                s += "\n";
            }
            Debug.Log(s);
        }
        #endregion
    }

    public void Init()
    {
        _pos.x = (TetrisTileManager.Instance.boardXSize - 4) / 2 + 3;
        _pos.y = 0;
        //CorrectionPos();
        SetPos();
    }

    //이미지를 잘라서 칠해져 있으면 배열에 할당 <- 이거 문제 생기면 그냥 인스펙터에서 관리하자 준이야
    public void CheckImage()
    {
        for (int x = 0; x < _col; x++)
        {
            for (int y = 0; y < _row; y++)
            {
                _tempPixels = _texture.GetPixels(y * _pixerSize, x * _pixerSize, _pixerSize, _pixerSize);
                for (int i = 0; i < _tempPixels.Length; i++)
                {
                    if (_tempPixels[i].a != 0)
                    {
                        _board[x, y] = 1;
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 이미지를 오른쪽으로 90도 회전시킵니다.
    /// </summary>
    public void RotateImage()
    {
        _rectTransform.rotation = Quaternion.Euler(0, 0, _rectTransform.eulerAngles.z - 90);

        //값복사
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
            {
                _tempboard[i, j] = _board[i, j];
            }
        }

        //배열 돌리기
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
            {
                _board[i, j] = _tempboard[j, _row - i - 1];
            }
        }
    }

    //인덱스 이동
    public void Move(BLOCKMOVEDIR dir)
    {
        switch (dir)
        {
            case BLOCKMOVEDIR.LEFT:
                _pos.x--;
                break;
            case BLOCKMOVEDIR.RIGHT:
                _pos.x++;
                break;
            case BLOCKMOVEDIR.UP:
                _pos.y--;
                break;
            case BLOCKMOVEDIR.DOWN:
                _pos.y++;
                break;
        }

        #region IndexoutofRange 예외처리
        _pos.x = Mathf.Clamp(_pos.x, 0, TetrisTileManager.Instance.boardXSize + BlockManager.empty_place_size);
        _pos.y = Mathf.Clamp(_pos.y, 0, TetrisTileManager.Instance.boardYSize + BlockManager.empty_place_size);
        #endregion

        SetPos();
    }

    //인덱스에 맞춰 포지션 적용
    public void SetPos()
    {
        Debug.Log($"{_pos.x} : {_pos.y}");
        float x = TetrisTileManager.Instance.tileParent.localPosition.x + _tileLength * _pos.x
            + (_spriteSize / 4 + _tileLength / 2) - 150;
        float y = TetrisTileManager.Instance.tileParent.localPosition.y - _tileLength * _pos.y
            - (_spriteSize / 4 + _tileLength / 2) + 150;
        _rectTransform.localPosition = new Vector3(x, y);
    }

    //Init후 위치 보정
    public void CorrectionPos()
    {
        XY tempPos = new XY { x = pos.x, y = pos.y };
        for (int i = 0; i < BlockManager.empty_place_size; i++)
        {
            tempPos.x++;
        }

        for (int i = 0; i < BlockManager.empty_place_size; i++)
        {
            tempPos.y++;
        }

        _pos.x = tempPos.x;
        _pos.y = tempPos.y;
    }

    /// <summary>
    /// 이동할 수 있지는 확인합니다.
    /// </summary>
    /// <param name="dir">이동할 방향</param>
    /// <returns>이동이 가능하면 true 아니면 false를 리턴합니다.</returns>
    public bool CanGo(BLOCKMOVEDIR dir)
    {
        XY tempPos = new XY { x = _pos.x, y = _pos.y };

        switch (dir)
        {
            case BLOCKMOVEDIR.DOWN:
                tempPos.y++;
                break;
            case BLOCKMOVEDIR.UP:
                tempPos.y--;
                break;
            case BLOCKMOVEDIR.RIGHT:
                tempPos.x++;
                break;
            case BLOCKMOVEDIR.LEFT:
                tempPos.x--;
                break;
        }

        for (int k = 0; k < 4; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (_board[k, l] == 1 && BlockManager.Instance.board[tempPos.x, tempPos.y] == 1)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool OverBlockXLeft()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && pos.x + i < 3)
                    return false;
            }
        }
        return true;
    }

    public bool OverBlockXRight()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && pos.x + i > TetrisTileManager.Instance.boardXSize + 6)
                    return false;
            }
        }
        return true;
    }

    public bool OverBlockYUp()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && pos.y + j < 3)
                    return false;
            }
        }
        return true;
    }


    public bool OverBlockYDown()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && pos.y + j > TetrisTileManager.Instance.boardYSize + 6)
                    return false;
            }
        }
        return true;
    }
}