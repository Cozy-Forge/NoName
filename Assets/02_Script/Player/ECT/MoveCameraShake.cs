using Cinemachine;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class MoveCameraShake : MonoBehaviour
{

    private PlayerController _controller;
    private CinemachineImpulseSource _soucre;

    private bool _isNotShake = true;

    private void Awake()
    {

        _controller = transform.root.GetComponent<PlayerController>();
        _soucre = GetComponent<CinemachineImpulseSource>();

        _controller.OnMoveEvent += HandlePlayerMove;

    }


    private void OnDestroy()
    {

        if (_controller == null) return;

        _controller.OnDashEvent -= HandlePlayerMove;

    }


    private void HandlePlayerMove(Vector2 value)
    {

        if (_isNotShake && value != Vector2.zero)
        {

            _isNotShake = false;
            _soucre.GenerateImpulse(0.1f);

            FAED.InvokeDelay(() =>
            {

                _isNotShake = true;

            }, 0.3f);

        }

    }
}
