using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // 로딩 씬을 통한 전환을 위한 정보를 담을 매니저
    // 이전 씬, 현재 씬, 다음 씬의 정보를 포함하고 있음

    public static SceneManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public string prevScene;
    public string currentScene;
    public string nextScene;
}
