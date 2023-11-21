using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPauseUI : MonoBehaviour
{
    
    public void Pause()
    {



    }

    public void RePause()
    {



    }

    public void Exit()
    {

        LoadingSceneController.LoadScene("LoadingScene");

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
