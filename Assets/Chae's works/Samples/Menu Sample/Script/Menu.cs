using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectPanel;

    public float animationDuration = 0.5f; // �ִϸ��̼� ���� �ð�

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
        SoundManager.instance.PlayMusic("Tuesday"); // �޴� �׸� ���� ���
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
