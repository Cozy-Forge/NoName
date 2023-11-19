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

        transform.localScale = new Vector3(1, 0, 1);

        _text.text = damage.ToString();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(1, 0.3f).SetEase(Ease.OutBounce));
        seq.Join(transform.DOMoveY(transform.position.y + 0.5f, 0.5f).SetEase(Ease.OutBounce));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DOScaleY(0, 0.2f).SetEase(Ease.OutBounce));
        seq.AppendCallback(() =>
        {

            FAED.InsertPool(gameObject);

        });

    }

}
