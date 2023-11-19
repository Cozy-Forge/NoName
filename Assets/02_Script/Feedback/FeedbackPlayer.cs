using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedbackPlayer : MonoBehaviour
{

    protected HashSet<Feedback> _feedbackContainer = new();

    public void PlayFeedback(float damage)
    {

        foreach (var feedback in _feedbackContainer)
        {

            feedback.Play(damage);

        }


    }

    public Coroutine AddCoroutine(IEnumerator coroutine)
    {

        return StartCoroutine(coroutine);

    }

    public void RemoveCoroutine(Coroutine coroutine)
    {

        StopCoroutine(coroutine);

    }

}
