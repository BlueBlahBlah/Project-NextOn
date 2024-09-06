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

    public void ChangeScene()
    {

    }
}
