using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string _nextScene;
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        if (!string.IsNullOrEmpty(_nextScene))
        {
            StartCoroutine(LoadSceneProgress());
        }
        else
        {
            Debug.LogError("Next scene is not specified!");
        }
    }

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            yield return new WaitForSeconds(0.02f);
            if (op.progress < 0.8f)
            {
                _progressBar.fillAmount = op.progress;
                yield return null;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (_progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
