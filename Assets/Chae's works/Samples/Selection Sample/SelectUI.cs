using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectPanel;

    private Image stageImage;

    private string stageName;
    private TextMeshProUGUI stageInfo;
    public float animationDuration = 0.5f; // 애니메이션 지속 시간

    private VolumeController volumeController;

    private void Start()
    {
        if (volumeController == null)
        {
            // 새로운 GameObject를 만들고 VolumeController를 추가
            if (GameObject.Find("VolumeController") != null)
            {
                volumeController = GameObject.Find("VolumeController").GetComponent<VolumeController>();
            }
            else
            {
                GameObject volumeControllerObject = new GameObject("VolumeController");
                volumeController = volumeControllerObject.AddComponent<VolumeController>();
            }

        }

        Invoke("TriggerFadeOut", 2f);
        Invoke("PlayBGM", 1.5f);
    }

    private void OnEnable()
    {
        // 씬 변경 시 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 변경 시 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Selection Scene")
        {
           
        }
    }

    public void SetStageName(string newStageName)
    {
        stageName = newStageName;
    }

    public void SetStageImage(string newStageName)
    {

    }

    public void SetStageInfo(string newStageName)
    {

    }

    

    public void OpenSelectUI(string newStageName)
    {
        // 이미지, 텍스트 등 수정
        SetStageName(newStageName);
        SetStageImage(newStageName);
        SetStageInfo(newStageName);

        OpenPanel();
    }

    public void CloseSelectUI()
    {
        ClosePanel();
    }

    public void ChangeScene()
    {
        volumeController.TriggerFadeIn();

        Invoke("DoChangeScene", 1.5f);
    }

    private void TriggerFadeOut()
    {
        volumeController.TriggerFadeOut();
    }

    private void PlayBGM()
    {
        SoundManager.instance.PlayMusic("Zensen he Totugekiseyo");
    }

    public void FadeIn()
    {
        // 까맣게 바뀌기
    }

    public void DoChangeScene()
    {
        LoadingManager.ToLoadScene();
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
