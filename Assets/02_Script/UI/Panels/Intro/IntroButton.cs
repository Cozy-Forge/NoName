using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class IntroButton : MonoBehaviour, IPointerEnterHandler
{
    public RectTransform RectTrm    { get; private set; }
    public TMP_Text Text            { get; private set; }

    private readonly Color _disableColor  = Color.gray;
    private readonly Color _enableColor   = Color.white;

    private readonly Vector3 _popScale = Vector3.one * 1.5f;
    private readonly Vector3 _disableScale = Vector3.one * 0.9f;

    private readonly float PopTextTime = 0.5f;

    Sequence mySeq;
    public event Action HoverEvent;

    private void Awake()
    {
        RectTrm = GetComponent<RectTransform>();
        Text = transform.Find("Text").GetComponent<TMP_Text>();
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
        mySeq = DOTween.Sequence();
        mySeq.Append(Text.rectTransform.DOScale(_popScale, PopTextTime)).SetEase(Ease.OutBounce);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverEvent?.Invoke();
    }
}
