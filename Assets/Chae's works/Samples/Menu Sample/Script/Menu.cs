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
            // 새로운 GameObject를 만들고 VolumeController를 추가
            GameObject volumeControllerObject = new GameObject("VolumeController");
            volumeController = volumeControllerObject.AddComponent<VolumeController>();


        }

        Invoke("TriggerFadeOut", 2f);
        Invoke("PlayBGM", 4);
    }


    public void ChangeScene()
    {
        // Scene 변경을 위한 함수
        // 1. SceneManager 인스턴스에 접근해 nextScene 을 이동하고자 하는 씬(인게임)으로 변경
        // 2. Loading Scene 으로 이동한 뒤 로딩을 거쳐 2차적으로 nextScene 으로 이동

        LoadingManager.ToLoadScene();
    }

    private void PlayBGM()
    {
        SoundManager.instance.PlayMusic("Redemption"); // 메뉴 테마 음악 재생
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
