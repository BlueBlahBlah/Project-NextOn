using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneContainer.instance.currentScene = "Menu Scene";
        SceneContainer.instance.nextScene = "Scenario1 Scene";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        // Scene ������ ���� �Լ�
        // 1. SceneManager �ν��Ͻ��� ������ nextScene �� �̵��ϰ��� �ϴ� ��(�ΰ���)���� ����
        // 2. Loading Scene ���� �̵��� �� �ε��� ���� 2�������� nextScene ���� �̵�

        LoadingManager.ToLoadScene();
    }
}
