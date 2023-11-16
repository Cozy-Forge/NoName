using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlinkFeedback : Feedback
{

    private readonly int HASH_BLINK = Shader.PropertyToID("_AddColorFade");

    private SpriteRenderer _spriteRenderer;
    private Coroutine _coroutine;
    private FeedbackDataContainer _data;

    public new struct FeedbackDataContainer
    {

        public float blinkTime;

    }

    public HitBlinkFeedback(FeedbackPlayer feedbackPlayer, FeedbackDataContainer? data = null) : base(feedbackPlayer)
    {

        //부모 & 부모에 없으면 자식꺼 가져오기
        _spriteRenderer = _transform.GetComponentInChildren<SpriteRenderer>();
        
        if(data != null)
        {

            _data = data.Value;

        }
        else
        {

            _data.blinkTime = 0.15f;

        }

    }

    public override void Play(float damage)
    {

        if(_coroutine == null)
        {

            _coroutine = _feedbackPlayer.AddCoroutine(BlinkCo());

        }
        else
        {

            _feedbackPlayer.RemoveCoroutine(_coroutine);
            _coroutine = _feedbackPlayer.AddCoroutine(BlinkCo());

        }


    }

    private IEnumerator BlinkCo()
    {

        _spriteRenderer.material.SetFloat(HASH_BLINK, 1);
        yield return new WaitForSeconds(_data.blinkTime);
        _spriteRenderer.material.SetFloat(HASH_BLINK, 0);

    }

}
