using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneContainer.instance.nextScene == null) SceneContainer.instance.nextScene = "Menu Scene";

        StartCoroutine("LoadScene");

        if (SoundManager.instance != null) SoundManager.instance.StopMusic();
    }

    public static void ToLoadScene()
    {
        SceneManager.LoadScene("Loading Scene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneContainer.instance.nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            Debug.Log($"{op.progress}");
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else if (timer <= 3f) // 현재 로딩 시작과 동시에 로딩 progress 가 90%를 달성하기 때문에, 로딩바를 자연스럽게 할 페이크 로딩 구간 3초 설정
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, 0.5f);
            }
            else if (timer >= 3f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, 1f);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
