using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject selectUI;

    private Image stageImage;

    private string stageName;
    private TextMeshProUGUI stageInfo;

    private VolumeController volumeController;

    private void Start()
    {
        if (volumeController == null)
        {
            // ���ο� GameObject�� ����� VolumeController�� �߰�
            GameObject volumeControllerObject = new GameObject("VolumeController");
            volumeController = volumeControllerObject.AddComponent<VolumeController>();
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

        selectUI.SetActive(true);
    }

    public void CloseSelectUI()
    {
        selectUI.SetActive(false);
    }

    public void ChangeSceneUI()
    {
        volumeController.TriggerFadeIn();

        Invoke("ChangeScene", 1.5f);
    }

    public void FadeInBlack()
    {

    }

    public void ChangeScene()
    {
        SceneContainer.instance.nextScene = "NewDemoStage";
        LoadingManager.ToLoadScene();
    }
}
