using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    //시작하면 효과 적용
    [SerializeField]
    private List<IntroButton> _btnLists = new List<IntroButton>();

    private int _currentIndex = 0;

    private void Awake()
    {
        foreach(var btn in _btnLists)
        {
            btn.Activate(false);
            btn.HoverEvent += () =>
            {
                foreach (var bt in _btnLists)
                {
                    if (bt == btn)
                        bt.Activate(true);
                    else
                        bt.Activate(false);
                }
            };
        }
        _btnLists[0].Activate(true);
    }
}
