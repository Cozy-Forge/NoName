using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [Header("IntroUIController")]
    [SerializeField]
    private PlayerUIReader _introUIController;

    [Header("IntroUI Info")]
    [SerializeField]
    private List<IntroButton> _btnLists = new List<IntroButton>();

    private int _currentIndex = 0;
    private bool _isOpenOtherPanel = false;
    // Setting Panel 열었을 때 조작 안 되도록 수정하기

    private void Awake()
    {
        foreach(var btn in _btnLists)
        {
            btn.Activate(false);
            btn.HoverEvent += () =>
            {
                for(int i = 0; i < _btnLists.Count; ++i)
                {
                    var bt = _btnLists[i];

                    if (bt == btn)
                    {
                        bt.Activate(true);
                        _currentIndex = i;
                    }
                    else
                        bt.Activate(false);
                }
            };
        }
        _btnLists[0].Activate(true);

        _introUIController.OnKeyDown += HandleSelectBtn;
        _introUIController.OnUISelect += HandleUISelect;
    }

    private void HandleUISelect()
    {
        _introUIController.SetEnable(false);
        _btnLists[_currentIndex].DoClickEvent();
    }

    private void HandleSelectBtn(PlayerUIReader.InputUIBtnDir dir)
    {
        if (dir == PlayerUIReader.InputUIBtnDir.Up)
        {
            _currentIndex = Math.Max(0, _currentIndex - 1);
        }
        else if(dir == PlayerUIReader.InputUIBtnDir.Down)
        {
            _currentIndex = Math.Min(_currentIndex + 1, _btnLists.Count - 1);
        }

        Debug.Log(_currentIndex);
        for(int i = 0; i < _btnLists.Count; ++i)
        {
            _btnLists[i].Activate(_currentIndex == i);
        }
    }
}
