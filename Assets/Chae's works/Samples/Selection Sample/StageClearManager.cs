using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static StageClearManager instance { get; private set; }

    // Stage Ŭ���� ���� ����
    private bool[] stageClearStatus = new bool[4]; // 0: Stage1, 1: Stage2, 2: Stage3, 3: Stage4

    private void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǿ ��ü ����

        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �ִٸ� �ı�
        }
    }

    private void OnEnable()
    {
        // �� ���� �� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� ���� �� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�Ǿ��� �� ȣ��Ǵ� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Selection Scene")
        {
            CheckStageClearStatus();
        }
    }

    // �� Stage Ŭ���� ���� Ȯ�� �� �Լ� ȣ�� (���⼭�� Debug �α� ���)
    private void CheckStageClearStatus()
    {
        for (int i = 0; i < stageClearStatus.Length; i++)
        {
            if (stageClearStatus[i])
            {
                // i��° �������� Ŭ���� -> i��° �������� ���� ������Ʈ �ı�
                ExecuteStageClearFunction(i + 1);
            }
            else
            {
                // i��° �������� ��Ŭ����
            }
        }
    }

    // �� Stage �� Ŭ���� �Ǿ��ٸ� ������ �Լ�
    private void ExecuteStageClearFunction(int stageNumber)
    {
        Debug.Log($"Stage {stageNumber} Ŭ���� �� ������ �Լ� ȣ��");

        // "Scene Change Object - i" ��� �̸��� ������Ʈ�� ã��
        string objectName = $"Scene Change Object - {stageNumber}";
        GameObject targetObject = GameObject.Find(objectName);

        if (targetObject != null)
        {
            // ������Ʈ�� ������ �� �ı�
            Destroy(targetObject);
        }
        else
        {
            // ������Ʈ�� �������� �ʴ´ٸ� ��ŵ
            
        }
    }


    // Stage Ŭ���� ���� ���� �޼��� (�ʿ� �� �ܺο��� ȣ��)
    public void SetStageClear(int stageNumber, bool isClear = true)
    {
        if (stageNumber >= 1 && stageNumber <= 4)
        {
            stageClearStatus[stageNumber - 1] = isClear;
            Debug.Log($"Stage {stageNumber} Ŭ���� ���°� {isClear}�� ������");
        }
        else
        {
            Debug.LogWarning("�߸��� Stage ��ȣ");
        }
    }
}

