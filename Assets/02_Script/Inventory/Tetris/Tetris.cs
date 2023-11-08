using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris : MonoBehaviour
{
    [Header("��ü ���� ������")]
    [SerializeField] private int _boardXSize;
    [SerializeField] private int _boardYSize;

    [Header("������")]
    [SerializeField] private Transform _tileParent;         //Ÿ�� �� ���� �θ�
    [SerializeField] private GameObject _tileLineParent;    //�� ���� �θ� <- Vertical �������� ����
    [SerializeField] private GameObject _tileImage;         //Ÿ��

    [Header("����")]
    [SerializeField] private Vector2 _endPos;               //�ִ� �������϶� �θ� ��ġ

    private Transform _tempTrm;

    private static int ScreenX = 10;
    private static int ScreenY = 20;

    private void Awake()
    {
        SettingBoard(); // ó�� ���� ����
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
    /// ���� ����� ���� ���� ����
    /// </summary>
    public void SettingBoard()
    {
        //����ó��
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

    //������ �߾��� �׻� ��� �ְ� �ϱ�
    public void CorrectionPos()
    {
        float x = Mathf.Lerp(0, _endPos.x, 1 - (float)(ScreenX - _boardXSize) / (float)ScreenX);
        float y = Mathf.Lerp(0, _endPos.y, 1 - (float)(ScreenY - _boardYSize) / (float)ScreenY);

        _tileParent.localPosition = new Vector2(x, y);
    }
}