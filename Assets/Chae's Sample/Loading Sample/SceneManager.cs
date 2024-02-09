using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // �ε� ���� ���� ��ȯ�� ���� ������ ���� �Ŵ���
    // ���� ��, ���� ��, ���� ���� ������ �����ϰ� ����

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
