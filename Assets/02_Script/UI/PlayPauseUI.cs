using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPauseUI : MonoBehaviour
{

    [SerializeField] private GameObject _obj;

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {

            Pause();

        }

    }

    public void Pause()
    {

        Time.timeScale = 0f;
        _obj.SetActive(true);

    }

    public void RePause()
    {

        Time.timeScale = 1;
        _obj.SetActive(false);

    }

    public void Exit()
    {

        LoadingSceneController.LoadScene("00_IntroScene");

    }

    public void Replay()
    {

        LoadingSceneController.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void OnDestroy()
    {
        
        Time.timeScale = 1;

    }

}
