using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartZone : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _blinder;

    [SerializeField]
    private PlayerInputReader _introPlayerInputReader;

    private bool _isStart = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameStart();
    }

    private void GameStart()
    {
        if (_isStart)
            return;

        _isStart = true;
        _introPlayerInputReader.InputData.Disable();

        _player.DOJump(transform.position, 2, 1, 0.25f)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    _blinder.SetActive(true);

                    //SceneChange
                    SceneManager.LoadScene(1); // юс╫ц
                });
    }
}
