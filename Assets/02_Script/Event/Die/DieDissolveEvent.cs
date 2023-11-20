using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieDissolveEvent : MonoBehaviour
{

    private readonly int HASN_DISSOLVE = Shader.PropertyToID("_FullGlowDissolveFade");

    [SerializeField] private UnityEvent _onDissolveEnd;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private HPObject _hpObject;

    private void Awake()
    {
        
        _hpObject = GetComponent<HPObject>();
        _hpObject.OnDieEvent += HandleDie;

        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError($"{gameObject.name}'s DieDissolveEvent : SpriteRenderer is null");
        }
    }

    private void HandleDie()
    {

        StartCoroutine(DissolveCo());

    }

    private IEnumerator DissolveCo()
    {

        float per = 0;
        while (per < 1)
        {

            per+= Time.deltaTime * 3;
            _spriteRenderer.material.SetFloat(HASN_DISSOLVE, 1 - per);
            yield return null;

        }

        _onDissolveEnd?.Invoke();

        Destroy(gameObject);

    }

}
