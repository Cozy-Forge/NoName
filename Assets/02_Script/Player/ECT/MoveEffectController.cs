using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffectController : MonoBehaviour
{

    private PlayerController _controller;
    private ParticleSystem _particle;

    private void Awake()
    {

        _controller = transform.root.GetComponent<PlayerController>();
        _particle = GetComponent<ParticleSystem>();

        _controller.OnMoveEvent += HandlePlayerMove;
        _controller.OnStateChangeEvent += HandleStateChanged;

    }

    private void HandleStateChanged(EnumPlayerState oldState, EnumPlayerState newState)
    {

        if(oldState == EnumPlayerState.Move)
        {

            _particle.Stop();

        }

        if(newState == EnumPlayerState.Move)
        {

            _particle.Play();

        }

    }

    private void OnDestroy()
    {

        if (_controller == null) return;

        _controller.OnDashEvent -= HandlePlayerMove;
        _controller.OnStateChangeEvent -= HandleStateChanged;


    }


    private void HandlePlayerMove(Vector2 value)
    {

        _particle.transform.localScale = value.x switch
        {

            var x when x > 0 => new Vector3(-1, 1, 1),
            var x when x < 0 => new Vector3(1, 1, 1),
            _ => _particle.transform.localScale,

        };

        if(value == Vector2.zero)
        {

            _particle.Stop();

        }
        else if(!_particle.isPlaying)
        {

            _particle.Play();

        }

    }
}
