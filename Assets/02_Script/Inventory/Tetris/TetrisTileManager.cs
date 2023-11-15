using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisTileManager : MonoBehaviour
{
    public static TetrisTileManager Instance;

    [Header("��ü ���� ������")]
    [SerializeField] private int _boardXSize;
    public int boardXSize => _boardXSize;
    [SerializeField] private int _boardYSize;
    public int boardYSize => _boardYSize;


    [Header("������Ʈ")]
    [SerializeField] private RectTransform _tileParent;     //Ÿ�� �� ���� �θ�
    public RectTransform tileParent => _tileParent;
    [SerializeField] private RectTransform _weapon;        //�� ģ���ڽ����� ��� ��ȯ

    [Header("����")]
    [SerializeField] private Vector2 _endPos;               //�ִ� �������϶� �θ� ��ġ

    [Header("�������� ������")]
    [SerializeField] private float _speed;
    public float speed => _speed;

    private Transform _tempTrm;

    private static int screenX = 10;                        //���� �ִ� ũ��X
    private static int screenY = 20;                        //���� �ִ� ũ��Y

    private void Awake()
    {
        #region �̱���
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError($"{transform} : TetrisTile is Multiple running!");
        }
        #endregion

        #region ����ó��
        if (_tileParent == null)
            Debug.LogError($"{transform} : tileParent is null!");

        if (_weapon == null)
            Debug.LogError($"{transform} : weapon is null");
        #endregion

        SettingBoard(); // ó�� ���� ����
    }

    //���� ���� ����
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
    /// ���� ����� ���� ���� ����
    /// </summary>
    public void SettingBoard()
    {
        //����ó��
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

    //������ �߾��� �׻� ��� �ְ� �ϱ�
    public void CorrectionPos()
    {
        float x = Mathf.Lerp(0, _endPos.x, 1 - (float)(screenX - _boardXSize) / (float)screenX);
        float y = Mathf.Lerp(0, _endPos.y, 1 - (float)(screenY - _boardYSize) / (float)screenY);

        _tileParent.localPosition = new Vector2(x, y);
    }

    //������ X����� ������ŵ�ϴ�.
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
            Debug.LogError($"{transform} : �߸��� ���� ��ġ - ����X����� �ʹ� Ŀ��");
        }
    }

    //������ Y����� ������ŵ�ϴ�.
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
            Debug.LogError($"{transform} : �߸��� ���� ��ġ - ����Y����� �ʹ� Ŀ��");
        }
    }
}