using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigFroggyHPBar : MonoBehaviour
{

    private Slider _slider;
    private HPObject _hpObject;

    private void Awake()
    {
        
        _slider = GetComponent<Slider>();
        _hpObject = FindObjectOfType<BigFroggyController>().GetComponent<HPObject>();

    }

    private void Update()
    {
        
        _slider.value = _hpObject.CurrentHP / _hpObject.MaxHP;

    }

}
