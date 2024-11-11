using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearPanel : MonoBehaviour
{
    // Scene ���� ����
    [SerializeField]
    private RectTransform selectPanel;
    public float animationDuration = 0.5f; // �ִϸ��̼� ���� �ð�

    [SerializeField]
    private VolumeController volumeController;

    // Stage Ŭ���� ���� ����
    [SerializeField]
    private int currentStageNumber = 0;

    // Button
    [SerializeField]
    private Button clearButton;

    private void Start()
    {
        if (volumeController == null)
        {
            GameObject volumeControllerObject = new GameObject("VolumeController");
            volumeController = volumeControllerObject.AddComponent<VolumeController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageClear()
    {
        
        if (StageClearManager.instance != null) // ���� StageClearManager �ν��Ͻ��� �����Ѵٸ�
        {
            // �������� Ŭ���� �Լ��� �ҷ��� �ش� ���������� Ŭ����
            StageClearManager.instance.SetStageClear(currentStageNumber, true);
            StageClearManager.instance.isSuccess = true;
        }
        else
        {
            Debug.Log("StageClearManager �� �����ϴ�!");
        }
    }
    
    public void ChangeScene()
    {
        StageClear();
        volumeController.TriggerFadeIn();

        Invoke("DoChangeScene", 1.5f);
    }    

    public void DoChangeScene()
    {

        if (SceneContainer.instance != null)
        {
            SceneContainer.instance.nextScene = "Selection Scene";
            LoadingManager.ToLoadScene();
        }
    }



    public void OpenPanel()
    {
        StartCoroutine(OpenPanelCoroutine());
        Invoke("ActivateButton", 1f);
    }

    private void ActivateButton()
    {
        clearButton.interactable = true;
    }
    

    // �ִϸ��̼� ���� - ������ ���� ���� Ŭ���� �� ���� �̵�. �ݱ� �ʿ� x
    // �г� ���� �ִϸ��̼�
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

    private float EaseOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
