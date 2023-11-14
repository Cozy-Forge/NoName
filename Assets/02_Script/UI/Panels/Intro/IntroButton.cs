using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IntroButton : MonoBehaviour, IPointerEnterHandler
{
    public RectTransform RectTrm    { get; private set; }
    public TMP_Text Text            { get; private set; }
    private Button _btn;

    private readonly Color _disableColor  = Color.gray;
    private readonly Color _enableColor   = Color.white;

    private readonly Vector3 _popScale = Vector3.one * 1.5f;
    private readonly Vector3 _disableScale = Vector3.one * 0.9f;

    private readonly float PopTextTime = 0.5f;

    Sequence mySeq;
    public event Action HoverEvent;
    public event Action BtnClickEvent;

    private void Awake()
    {
        RectTrm = GetComponent<RectTransform>();
        _btn = GetComponent<Button>();
        Text = transform.Find("Text").GetComponent<TMP_Text>();

        _btn.onClick.AddListener(() =>
        {
            Debug.Log(1);
            BtnClickEvent?.Invoke();
        });
    }

    public void Activate(bool value)
    {
        if(value)
        {
            Text.color = _enableColor;
            PopText();
        }
        else
        {
            mySeq.Kill();
            Text.color                      = _disableColor;
            Text.rectTransform.localScale   = _disableScale;
        }
    }

    private void PopText()
    {
        mySeq.Kill();
        mySeq = DOTween.Sequence();
        mySeq.Append(Text.rectTransform.DOScale(_popScale, PopTextTime)).SetEase(Ease.OutBounce);
    }

    public void DoClickEvent()
    {
        _btn.onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverEvent?.Invoke();
    }
}
