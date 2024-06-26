using DG.Tweening;
using FD.Dev;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpeechBubble : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private TMP_Text _tmp;
    private bool _isTextPlay;
    private bool _isReleasing;

    private void Awake()
    {
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tmp = GetComponentInChildren<TMP_Text>();

    }

    public void ShowText(string text)
    {
        if (_isTextPlay || _isReleasing) return;
        StartCoroutine(ShowCO(text));

    }

    public void ReleseText()
    {
        if(_isReleasing) return;

        DOTween.Kill(_tmp);
        StartCoroutine(ReleseCO());

    }

    private IEnumerator ReleseCO()
    {

        _isReleasing = true;

        yield return new WaitUntil(() => !_isTextPlay);

        float per = 0;
        Vector2 size = _spriteRenderer.size;
        _tmp.text = string.Empty;

        while (per < 1)
        {

            per += Time.deltaTime * 3;
            _spriteRenderer.size = Vector2.Lerp(size, new Vector2(0, 1.5f), 
                FAED.Easing(FAED_Easing.OutSine, per));

            yield return null;

        }

        _isReleasing = false;
        _spriteRenderer.size = new Vector2(0, 0);

    }

    private IEnumerator ShowCO(string text)
    {

        _isTextPlay = true; 

        float per = 0;

        while(per < 1)
        {

            per += Time.deltaTime * 3;
            _spriteRenderer.size = Vector2.Lerp(new Vector2(0, 1.5f), new Vector2(text.Length / 2, 1.5f), 
                FAED.Easing(FAED_Easing.OutSine, per));

            yield return null;

        }


        for(int i = 0; i < text.Length; i++)
        {

            _tmp.text += text[i];
            yield return new WaitForSeconds(0.015f);

        }

        yield return new WaitForSeconds(0.3f);

        _isTextPlay = false;

    }

}
