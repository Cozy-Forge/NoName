using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Panel : MonoBehaviour
{
    protected bool _isShow = false;
    protected bool _isAnim = false;
    protected RectTransform _rectTransform;

    private float _popupTime = 1f;
    protected Sequence _mySeq;

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Show(bool value)
    {
        _isShow = value;
        if (value == true)
            ShowOn();
        else
            ShowOff();
    }

    public virtual void ShowOn(bool isPopUpEffect = true)
    {
        if (_isAnim)
            return;

        if (isPopUpEffect)
            _isAnim = true;
        _isShow = true;
        gameObject.SetActive(true);

        _rectTransform.localScale = Vector3.zero;

        if (isPopUpEffect)
            PopupEffect();
    }

    public virtual void ShowOff()
    {
        _mySeq.Kill();
        _isAnim = false;
        _isShow = false;
        gameObject.SetActive(false);
    }

    public virtual void PopupEffect()
    {
        _rectTransform.localScale = Vector3.zero;

        _mySeq.Kill();
        _mySeq = DOTween.Sequence();
        _mySeq.Append(_rectTransform.DOScale(Vector3.one, _popupTime))
            .SetEase(Ease.OutBounce)
            .OnComplete(() => { _isAnim = false; });
    }

}
