using FD.Dev;
using UnityEngine;

public enum BLOCKMOVEDIR
{
    DOWN = 0,
    UP = 1,
    LEFT = 2,
    RIGHT = 3,
    UPTwo = 4,
    UPThree = 5,
    NONE = 6,
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

    [SerializeField] Weapon _weaponPrefab;
    public Weapon weaponPrefab => _weaponPrefab;

    public bool _isDebug = false;

    private Color[] _tempPixels;

    private void Awake()
    {
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

        if(weaponPrefab == null)
            Debug.LogError($"{transform} : weaponPrefab is null!");

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

    // 초기화
    public void Init()
    {
        _rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        _pos.x = (TetrisTileManager.Instance.boardXSize - 4) / 2 + 3;
        _pos.y = 0;

        while (!OverBlockXLeft(_pos))
        {
            _pos.x++;
        }

        while (!OverBlockYUp(_pos))
        {
            _pos.y++;
        }

        CheckImage();
        SetPos();
        if (!CollisionOtherBlock(BLOCKMOVEDIR.NONE))
        {
            BlockManager.Instance.SetSelectBlockNull();
            FAED.InsertPool(gameObject);
            PriortyQueueBlock.Instance.RandomPop();
        }

    }

    public void IncreaseXPos() => _pos.x++;
    public void IncreaseYPos() => _pos.y++;

    //이미지를 잘라서 칠해져 있으면 배열에 할당 <- 이거 문제 생기면 그냥 인스펙터에서 관리하자 준이야
    public void CheckImage()
    {
        for (int x = 0; x < _col; x++)
        {
            for (int y = 0; y < _row; y++)
            {
                _board[x, y] = 0;
            }
        }

        for (int x = 0; x < _col; x++)
        {
            for (int y = 0; y < _row; y++)
            {
                _tempPixels = _texture.GetPixels(y * _pixerSize, x * _pixerSize, _pixerSize, _pixerSize);
                for (int i = 0; i < _tempPixels.Length; i++)
                {
                    if (_tempPixels[i].a != 0)
                    {
                        _board[_col - x - 1, y] = 1;
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
                _board[i, j] = _tempboard[_col - j - 1, i];
            }
        }

        if (!CollisionOtherBlock(BLOCKMOVEDIR.NONE) || !OverBlockAllSide(_pos))
        {
            if (CollisionOtherBlock(BLOCKMOVEDIR.RIGHT) && OverBlockAllSide(new XY() { x = _pos.x + 1, y = _pos.y }))
            {
                _pos.x++;
            }
            else if (CollisionOtherBlock(BLOCKMOVEDIR.LEFT) && OverBlockAllSide(new XY() { x = _pos.x - 1, y = _pos.y }))
            {
                _pos.x--;
            }
            else if (CollisionOtherBlock(BLOCKMOVEDIR.UP) && OverBlockAllSide(new XY() { x = _pos.x, y = _pos.y - 1 }))
            {
                _pos.y--;
            }
            else if (CollisionOtherBlock(BLOCKMOVEDIR.UPTwo) && OverBlockAllSide(new XY() { x = _pos.x, y = _pos.y - 2 }))
            {
                _pos.y -= 2;
            }
            else if (CollisionOtherBlock(BLOCKMOVEDIR.UPThree) && OverBlockAllSide(new XY() { x = _pos.x, y = _pos.y - 3 }))
            {
                _pos.y -= 3;
            }
            else if (CollisionOtherBlock(BLOCKMOVEDIR.DOWN) && OverBlockAllSide(new XY() { x = _pos.x, y = _pos.y + 1 }))
            {
                _pos.y++;
            }
            else
            {
                //다 안되면 못돌려
                for (int i = 0; i < _row; i++)
                {
                    for (int j = 0; j < _col; j++)
                    {
                        _board[i, j] = _tempboard[i, j];
                    }
                }
                _rectTransform.rotation = Quaternion.Euler(0, 0, _rectTransform.eulerAngles.z + 90);
            }
            SetPos();
        }
    }

    //인덱스 이동
    public void Move(BLOCKMOVEDIR dir)
    {
        switch (dir)
        {
            case BLOCKMOVEDIR.LEFT:
                if (!CollisionOtherBlock(dir) || !CanGo(dir))
                    return;
                _pos.x--;
                break;
            case BLOCKMOVEDIR.RIGHT:
                if (!CollisionOtherBlock(dir) || !CanGo(dir))
                    return;
                _pos.x++;
                break;
            case BLOCKMOVEDIR.UP:
                if (!CollisionOtherBlock(dir) || !CanGo(dir))
                    return;
                _pos.y--;
                break;
            case BLOCKMOVEDIR.DOWN:
                if (!CanGo(dir))
                {
                    BlockManager.Instance.SetSelectBlockNull();
                    return;
                }
                else if (!CollisionOtherBlock(dir))
                {
                    BlockManager.Instance.SetSelectBlockNull();
                    FillBoard();
                    return;
                }
                _pos.y++;
                break;
        }

        SetPos();
    }

    //인덱스에 맞춰 포지션 적용
    public void SetPos()
    {
        float x = TetrisTileManager.Instance.tileParent.localPosition.x + _tileLength * _pos.x
            + (_spriteSize / 4 + _tileLength / 2) - _tileLength * 3;
        float y = TetrisTileManager.Instance.tileParent.localPosition.y - _tileLength * _pos.y
            - (_spriteSize / 4 + _tileLength / 2) + _tileLength * 3;
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
    public bool CanGo(BLOCKMOVEDIR dir, bool isAdd = true)
    {
        
        XY tempPos = new XY { x = _pos.x, y = _pos.y };

        switch (dir)
        {
            case BLOCKMOVEDIR.DOWN: //아래가 안되면 끝내야댐
                tempPos.y++;
                if (!OverBlockYDown(tempPos))
                {
                    FillBoard(isAdd);
                    BlockManager.Instance.SetSelectBlockNull();
                    return false;
                }
                break;
            case BLOCKMOVEDIR.UP:
                tempPos.y--;
                if (!OverBlockYUp(tempPos))
                {
                    return false;
                }
                break;
            case BLOCKMOVEDIR.RIGHT:
                tempPos.x++;
                if (!OverBlockXRight(tempPos))
                {
                    return false;
                }
                break;
            case BLOCKMOVEDIR.LEFT:
                tempPos.x--;
                if (!OverBlockXLeft(tempPos))
                {
                    return false;
                }
                break;
        }

        return true;
    }

    /// <summary>
    /// 매개 뱐수 Pos의 값을 기준으로 왼쪽 벽을 넘었는지 확인합니다.
    /// </summary>
    /// <param name="tempPos">이동된 위치</param>
    /// <returns>벽을 넘으면 false, 벽을 넘지 않으면 true를 리턴합니다.</returns>
    public bool OverBlockXLeft(XY tempPos)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && tempPos.x + j < 3)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 매개 뱐수 Pos의 값을 기준으로 오른쪽 벽을 넘었는지 확인합니다.
    /// </summary>
    /// <param name="tempPos">이동된 위치</param>
    /// <returns>벽을 넘으면 false, 벽을 넘지 않으면 true를 리턴합니다.</returns>
    public bool OverBlockXRight(XY tempPos)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && tempPos.x + j >= TetrisTileManager.Instance.boardXSize + 3)
                    return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 매개 뱐수 Pos의 값을 기준으로 위쪽 벽을 넘었는지 확인합니다.
    /// </summary>
    /// <param name="tempPos">이동된 위치</param>
    /// <returns>벽을 넘으면 false, 벽을 넘지 않으면 true를 리턴합니다.</returns>
    public bool OverBlockYUp(XY tempPos)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && tempPos.y + i < 3)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 매개 뱐수 Pos의 값을 기준으로 아래쪽 벽을 넘었는지 확인합니다.
    /// </summary>
    /// <param name="tempPos">이동된 위치</param>
    /// <returns>벽을 넘으면 false, 벽을 넘지 않으면 true를 리턴합니다.</returns>
    public bool OverBlockYDown(XY tempPos)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 1 && tempPos.y + i >= TetrisTileManager.Instance.boardYSize + 3)
                    return false;
            }
        }
        return true;
    }

    public bool OverBlockAllSide(XY tempPos)
    {
        if (OverBlockYDown(tempPos) && OverBlockXRight(tempPos) && OverBlockYUp(tempPos) && OverBlockXLeft(tempPos))
            return true;
        return false;
    }

    /// <summary>
    /// 다른 블록과 충돌하는지 확인합니다.
    /// </summary>
    /// <param name="dir">이동할 방향</param>
    /// <returns>충돌이 일어나면 false, 아니면 true를 리턴합니다.</returns>
    public bool CollisionOtherBlock(BLOCKMOVEDIR dir)
    {
        XY tempPos = new XY() { x = pos.x, y = pos.y };

        switch (dir)
        {
            case BLOCKMOVEDIR.LEFT:
                tempPos.x--;
                break;
            case BLOCKMOVEDIR.RIGHT:
                tempPos.x++;
                break;
            case BLOCKMOVEDIR.UP:
                tempPos.y--;
                break;
            case BLOCKMOVEDIR.DOWN:
                tempPos.y++;
                break;
            case BLOCKMOVEDIR.UPTwo:
                tempPos.y += 2;
                break;
            case BLOCKMOVEDIR.UPThree:
                tempPos.y += 3;
                break;
            default:
                break;
        }

        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                if (board[i, j] == 1 && BlockManager.Instance.board[tempPos.y + i, tempPos.x + j] == 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 이동 멈추면 1로 채우고 우선순위 리스트에 넣어줍니다. 만약 그 자리에 다른 블록이 있으면 제거하고 추가로 랜덤으로 3개의 블록을 제거합니다.
    /// </summary>
    public void FillBoard(bool isAdd = true)
    {
        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                if (board[i, j] == 1)
                {
                    if (BlockManager.Instance.board[_pos.y + i, _pos.x + j] == 1)
                    {
                        BlockManager.Instance.EraseBoard(this);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                if (board[i, j] == 1)
                {
                    BlockManager.Instance.board[_pos.y + i, _pos.x + j] = 1;
                }
            }
        }
        if (isAdd)
        {
            GameObject.Find("Player").GetComponent<PlayerWeaponContainer>().AddWeapon(Instantiate(_weaponPrefab));
            PriortyQueueBlock.Instance.Push(this);
        }
    }

    public void DebugArr()
    {
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                s += _board[i, j];
            }
            s += '\n';
        }
        Debug.Log(s);
    }

    public void ClearBoard()
    {
        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                if (board[i, j] == 1)
                {
                    BlockManager.Instance.board[_pos.y + i, _pos.x + j] = 0;
                }
            }
        }
    }
}