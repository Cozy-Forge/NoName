using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFeedbackPlayer : FeedbackPlayer
{

    private void Awake()
    {

        var hitBlinkFeedback = new HitBlinkFeedback(this);

        _feedbackContainer.Add(hitBlinkFeedback);

    }

}
