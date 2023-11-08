using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris : MonoBehaviour
{
    [Header("전체 보드 사이즈")]
    [SerializeField] private int _boardXSize;
    [SerializeField] private int _boardYSize;

    [Header("프리팹")]
    [SerializeField] private Transform _tileParent;         //타일 중 제일 부모
    [SerializeField] private GameObject _tileLineParent;    //그 다음 부모 <- Vertical 기준으로 정렬
    [SerializeField] private GameObject _tileImage;         //타일

    [Header("벡터")]
    [SerializeField] private Vector2 _endPos;               //최대 사이즈일때 부모 위치

    private Transform _tempTrm;

    private static int ScreenX = 10;
    private static int ScreenY = 20;

    private void Awake()
    {
        SettingBoard(); // 처음 보드 생성
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _boardXSize++;
            SettingBoard();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _boardYSize++;
            SettingBoard();
        }
    }

    /// <summary>
    /// 보드 사이즈에 맞춰 보드 세팅
    /// </summary>
    public void SettingBoard()
    {
        //예외처리
        _boardXSize = Mathf.Clamp(_boardXSize, 1, ScreenX);
        _boardYSize = Mathf.Clamp(_boardYSize, 1, ScreenY);


        int x = ScreenX - _boardXSize;
        int y = ScreenY - _boardYSize;
        for (int i = 0; i < y; i++)
        {
            _tileParent.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = y; i < ScreenY; i++)
        {
            _tempTrm = _tileParent.GetChild(i);
            _tempTrm.gameObject.SetActive(true);
            for (int j = 0; j < x; j++)
            {
                _tempTrm.GetChild(j).gameObject.SetActive(false);
            }
            for (int j = x; j < ScreenX; j++)
            {
                _tempTrm.GetChild(j).gameObject.SetActive(true);
            }
        }

        CorrectionPos();
    }

    //보드의 중앙이 항상 가운데 있게 하기
    public void CorrectionPos()
    {
        float x = Mathf.Lerp(0, _endPos.x, 1 - (float)(ScreenX - _boardXSize) / (float)ScreenX);
        float y = Mathf.Lerp(0, _endPos.y, 1 - (float)(ScreenY - _boardYSize) / (float)ScreenY);

        _tileParent.localPosition = new Vector2(x, y);
    }
}