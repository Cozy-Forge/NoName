using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFeedbackPlayer : FeedbackPlayer
{

    [SerializeField] private float _blinkTile = 0.03f;

    private void Awake()
    {

        var hitBlinkFeedback = new HitBlinkFeedback(this, new HitBlinkFeedback.FeedbackDataContainer { blinkTime = _blinkTile});

        _feedbackContainer.Add(hitBlinkFeedback);

    }

}
