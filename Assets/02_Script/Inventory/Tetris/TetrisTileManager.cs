using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisTileManager : MonoBehaviour
{
    public static TetrisTileManager Instance;

    [Header("전체 보드 사이즈")]
    [SerializeField] private int _boardXSize;
    public int boardXSize => _boardXSize;
    [SerializeField] private int _boardYSize;
    public int boardYSize => _boardYSize;


    [Header("오브젝트")]
    [SerializeField] private RectTransform _tileParent;     //타일 중 제일 부모
    public RectTransform tileParent => _tileParent;
    [SerializeField] private RectTransform _weapon;        //이 친구자식으로 블록 소환

    [Header("벡터")]
    [SerializeField] private Vector2 _endPos;               //최대 사이즈일때 부모 위치

    [Header("내려가는 딜레이")]
    [SerializeField] private float _speed;
    public float speed => _speed;

    private Transform _tempTrm;

    private static int screenX = 10;                        //보드 최대 크기X
    private static int screenY = 20;                        //보드 최대 크기Y

    private void Awake()
    {
        #region 싱글톤
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError($"{transform} : TetrisTile is Multiple running!");
        }
        #endregion

        #region 예외처리
        if (_tileParent == null)
            Debug.LogError($"{transform} : tileParent is null!");

        if (_weapon == null)
            Debug.LogError($"{transform} : weapon is null");
        #endregion

        SettingBoard(); // 처음 보드 생성
    }

    //여긴 지울 예정
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseBoardSizeX();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            IncreaseBoardSizeY();
        }
    }

    /// <summary>
    /// 보드 사이즈에 맞춰 보드 세팅
    /// </summary>
    public void SettingBoard()
    {
        //예외처리
        _boardXSize = Mathf.Clamp(_boardXSize, 1, screenX);
        _boardYSize = Mathf.Clamp(_boardYSize, 1, screenY);


        int x = screenX - _boardXSize;
        int y = screenY - _boardYSize;
        for (int i = 0; i < y; i++)
        {
            _tileParent.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = y; i < screenY; i++)
        {
            _tempTrm = _tileParent.GetChild(i);
            _tempTrm.gameObject.SetActive(true);
            for (int j = 0; j < x; j++)
            {
                _tempTrm.GetChild(j).gameObject.SetActive(false);
            }
            for (int j = x; j < screenX; j++)
            {
                _tempTrm.GetChild(j).gameObject.SetActive(true);
            }
        }

        CorrectionPos();
    }

    //보드의 중앙이 항상 가운데 있게 하기
    public void CorrectionPos()
    {
        float x = Mathf.Lerp(0, _endPos.x, 1 - (float)(screenX - _boardXSize) / (float)screenX);
        float y = Mathf.Lerp(0, _endPos.y, 1 - (float)(screenY - _boardYSize) / (float)screenY);

        _tileParent.localPosition = new Vector2(x, y);
    }

    //보드의 X사이즈를 증가시킵니다.
    public void IncreaseBoardSizeX()
    {
        if (_boardXSize < screenX)
        {
            _boardXSize++;
            SettingBoard();
            foreach (TetrisImg tetrisImg in PriortyQueueBlock.Instance._tetrisImgList)
            {
                tetrisImg.SetPos();
            }
        }
        else
        {
            Debug.LogError($"{transform} : 잘못된 접근 수치 - 보드X사이즈가 너무 커짐");
        }
    }

    //보드의 Y사이즈를 증가시킵니다.
    public void IncreaseBoardSizeY()
    {
        if(_boardYSize < screenY)
        {
            _boardYSize++;
            SettingBoard();
            foreach (TetrisImg tetrisImg in PriortyQueueBlock.Instance._tetrisImgList)
            {
                tetrisImg.IncreaseYPos();
                tetrisImg.SetPos();
            }
        }
        else
        {
            Debug.LogError($"{transform} : 잘못된 접근 수치 - 보드Y사이즈가 너무 커짐");
        }
    }
}