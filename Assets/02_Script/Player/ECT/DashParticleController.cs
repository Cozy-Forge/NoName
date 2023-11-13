using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DashParticleController : MonoBehaviour
{

    [SerializeField] private PlayerInputReader _inputReader;

    private PlayerController _controller;
    private ParticleSystem _particle;

    private void Awake()
    {
        
        _controller = transform.root.GetComponent<PlayerController>();
        _particle = GetComponent<ParticleSystem>();

        _controller.OnDashEvent += HandlePlayerDash;
        _controller.OnDashEndEvent += HandlePlayerDashEnd;

    }
    private void OnDestroy()
    {

        if (_controller == null) return;

        _controller.OnDashEvent -= HandlePlayerDash;
        _controller.OnDashEndEvent -= HandlePlayerDashEnd;

    }

    private void HandlePlayerDashEnd()
    {

        _particle.Stop();

    }

    private void HandlePlayerDash(Vector2 value)
    {

        _particle.transform.localScale = value.x switch
        {

            var x when x > 0 => new Vector3(-1, 1, 1),
            var x when x < 0 => new Vector3(1, 1, 1),
            _ => _particle.transform.localScale,

        };

        _particle.Play();

    }


}
