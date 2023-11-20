using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string _nextScene;

    [SerializeField]
    private Image _progressBar;

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op =  SceneManager.LoadSceneAsync(_nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                _progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (_progressBar.fillAmount > 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
