using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback
{

    protected FeedbackPlayer _feedbackPlayer;
    protected GameObject _gameObject;
    protected Transform _transform;
    public Feedback(FeedbackPlayer feedbackPlayer)
    {

        _feedbackPlayer = feedbackPlayer;
        _transform = _feedbackPlayer.transform;
        _gameObject = _feedbackPlayer.gameObject;

    }

    public struct FeedbackDataContainer { }

    public abstract void Play(float damage);

}
