using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContainer : MonoBehaviour
{
    // �ε� ���� ���� ��ȯ�� ���� ������ ���� �Ŵ���
    // ���� ��, ���� ��, ���� ���� ������ �����ϰ� ����

    public static SceneContainer instance = null;

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
