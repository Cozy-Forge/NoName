using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    private TMP_Text _text;

    private void Awake()
    {
        
        _text = GetComponent<TMP_Text>();

    }

    public void Set(float damage)
    {


        _text.text = damage.ToString();
        _text.color = Color.white;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + 1.5f, 0.4f).SetEase(Ease.OutQuad));
        seq.Join(_text.DOColor(Color.red, 0.4f).SetEase(Ease.InSine));
        seq.AppendCallback(() =>
        {

            FAED.InsertPool(gameObject);

        });

    }

}
