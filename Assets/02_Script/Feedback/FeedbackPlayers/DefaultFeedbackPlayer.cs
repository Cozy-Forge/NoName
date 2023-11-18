using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFeedbackPlayer : FeedbackPlayer
{

    [SerializeField] private float _blinkTile = 0.03f;
    [SerializeField] private DamageTextFeedback.FeedbackDataContainer _damageData;

    private void Awake()
    {

        var hitBlinkFeedback = new HitBlinkFeedback(this, new HitBlinkFeedback.FeedbackDataContainer { blinkTime = _blinkTile});
        var damageTextFeedback = new DamageTextFeedback(this, _damageData);

        _feedbackContainer.Add(hitBlinkFeedback);
        _feedbackContainer.Add(damageTextFeedback);

    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)_damageData.offset, _damageData.size);


    }

#endif

}
