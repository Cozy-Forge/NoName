using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempCredit : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _creditText;

    private void Awake()
    {
        string str = _creditText.text;
        _creditText.text = "";

        Sequence seq = DOTween.Sequence();
        seq.Append(_creditText.DOText(str, str.Length * 0.15f))
            .SetEase(Ease.Linear);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }
}
