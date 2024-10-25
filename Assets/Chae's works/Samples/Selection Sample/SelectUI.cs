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
    public float animationDuration = 0.5f; // �ִϸ��̼� ���� �ð�

    private VolumeController volumeController;

    private void Start()
    {
        if (volumeController == null)
        {
            // ���ο� GameObject�� ����� VolumeController�� �߰�
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
        // �� ���� �� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� ���� �� �̺�Ʈ ����
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
        // �̹���, �ؽ�Ʈ �� ����
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
        // ��İ� �ٲ��
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
        panel.gameObject.SetActive(true); // �г� Ȱ��ȭ

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
    }

    // �г� �ݱ� �ִϸ��̼�
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
        panel.gameObject.SetActive(false); // �г� ��Ȱ��ȭ
    }

    private float EaseOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
