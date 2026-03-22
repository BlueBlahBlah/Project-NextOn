using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectPanel;

    public float animationDuration = 0.5f; // 애니메이션 지속 시간

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
        SoundManager.instance.PlayMusic("Tuesday"); // 메뉴 테마 음악 재생
    }

    private void TriggerFadeOut()
    {
        volumeController.TriggerFadeOut();
    }

    public void OpenSetting() { OpenPanel(); }

    public void CloseSetting() { ClosePanel(); }

    public void DoExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else 
        Application.Quit();  
    #endif
    }

    private void OpenPanel()
    {
        StartCoroutine(OpenPanelCoroutine());
    }

    private void ClosePanel()
    {
        StartCoroutine(ClosePanelCoroutine());
    }

    private IEnumerator OpenPanelCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        RectTransform panel = selectPanel;
        panel.gameObject.SetActive(true); // 패널 활성화

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
    }

    // 패널 닫기 애니메이션
    private IEnumerator ClosePanelCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;
        RectTransform panel = selectPanel;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
        panel.gameObject.SetActive(false); // 패널 비활성화
    }

    private float EaseOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
