using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public GameObject Setting;

    private VolumeController volumeController;

    // Start is called before the first frame update
    void Start()
    {
        SceneContainer.instance.currentScene = "Menu Scene";
        SceneContainer.instance.nextScene = "Scenario1 Scene";

        if (volumeController == null)
        {
            // ���ο� GameObject�� ����� VolumeController�� �߰�
            GameObject volumeControllerObject = new GameObject("VolumeController");
            volumeController = volumeControllerObject.AddComponent<VolumeController>();


        }

        Invoke("TriggerFadeOut", 2f);
        Invoke("PlayBGM", 4);
    }


    public void ChangeScene()
    {
        // Scene ������ ���� �Լ�
        // 1. SceneManager �ν��Ͻ��� ������ nextScene �� �̵��ϰ��� �ϴ� ��(�ΰ���)���� ����
        // 2. Loading Scene ���� �̵��� �� �ε��� ���� 2�������� nextScene ���� �̵�

        LoadingManager.ToLoadScene();
    }

    private void PlayBGM()
    {
        SoundManager.instance.PlayMusic("Redemption"); // �޴� �׸� ���� ���
    }

    private void TriggerFadeOut()
    {
        volumeController.TriggerFadeOut();
    }

    public void OpenSetting() { Setting.SetActive(true); }

    public void CloseSetting() { Setting.SetActive(false); }

    public void DoExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else 
        Application.Quit();  
    #endif
    }
}
