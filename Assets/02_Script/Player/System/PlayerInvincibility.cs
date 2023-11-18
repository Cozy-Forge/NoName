using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{

    private PlayerController _controller;
    private PlayerHP _playerHP;
    private Collider2D[] _colliders;

    private void Awake()
    {
        
        _controller = GetComponent<PlayerController>();
        _playerHP = GetComponent<PlayerHP>();
        _colliders = GetComponentsInChildren<Collider2D>();

        _controller.OnDashEvent += HandleDash;
        _controller.OnDashEndEvent += HandleDashEnd;


    }

    private void HandleDash(Vector2 dir)
    {

        foreach (var collider in _colliders)
        {

            collider.enabled = false;

        }

        _playerHP.IsInvincibility = true;

    }

    private void HandleDashEnd()
    {


        foreach (var collider in _colliders)
        {

            collider.enabled = true;

        }

        _playerHP.IsInvincibility = false;

    }

}
