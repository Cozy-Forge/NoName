using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TetrisImg : MonoBehaviour
{
    private Texture2D       _texture;
    private RectTransform   _rectTransform;
    private int[,]          _board = new int[4,4];
    private int[,]          _tempboard = new int[4,4];

    const int _row = 4;
    const int _col = 4;
    const int _pixerSize = 16;
    const int _spriteSize = 200;

    public bool _isDebug = false;

    Color[]   _tempPixels;

    private void Awake()
    {
        _texture = Resources.Load<Texture2D>($"test/{transform.name}") as Texture2D;
        _rectTransform = GetComponent<RectTransform>();

        #region 예외처리
        if (!_texture.isReadable)
            Debug.LogError($"{transform} : This sprite can't read!");

        if(_texture == null)
            Debug.LogError($"{transform} : _texture path is wrong!");


        if(_rectTransform.rect.width != _spriteSize || _rectTransform.rect.height != _spriteSize)
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
            for(int i = _row - 1; i >= 0; i--)
            {
                for(int j = 0; j < _col; j++)
                {
                    s += _board[i, j].ToString() + " ";
                }
                s += "\n";
            }
            Debug.Log(s);
        }
        #endregion
    }

    //이미지를 잘라서 칠해져 있으면 배열에 할당 <- 이거 문제 생기면 그냥 인스펙터에서 관리하자 준이야
    public void CheckImage()
    {
        for(int x = 0; x < _col; x++)
        {
            for(int y = 0; y < _row; y++)
            {
                _tempPixels = _texture.GetPixels(y * _pixerSize, x * _pixerSize, _pixerSize, _pixerSize);
                for(int i = 0; i < _tempPixels.Length; i++)
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
        _rectTransform.eulerAngles = new Vector3(0, 0, _rectTransform.rotation.z + 90);

        //배열 돌리기
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
            {
                _tempboard[j, _row - 1 - i] = _board[i, j];
            }
        }
        _board = _tempboard;
    }
}