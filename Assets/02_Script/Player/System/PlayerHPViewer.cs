using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPViewer : MonoBehaviour
{

    private Slider _slider;
    private PlayerHP _playerHP;

    private void Awake()
    {
        
        _slider = GetComponent<Slider>();
        _playerHP = transform.root.GetComponent<PlayerHP>();

    }

    private void Update()
    {
        
        _slider.value = _playerHP.CurrentHP / _playerHP.MaxHP;

    }

}
