using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextFeedback : Feedback
{
    public DamageTextFeedback(FeedbackPlayer feedbackPlayer, FeedbackDataContainer data) : base(feedbackPlayer)
    {

        _data = data;

    }

    private FeedbackDataContainer _data;

    [System.Serializable]
    public new struct FeedbackDataContainer
    {

        public float size;
        public Vector2 offset;

    }

    public override void Play(float damage)
    {

        var size = Random.insideUnitSphere * _data.size;
        size.z = 0;

        FAED.TakePool<DamageText>("DamageText", _transform.position + (Vector3)_data.offset + size, Quaternion.identity).Set(damage);

    }
}
