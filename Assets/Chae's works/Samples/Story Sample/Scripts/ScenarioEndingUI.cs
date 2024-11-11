using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEndingUI : MonoBehaviour
{
    [Header("UI Object")]
    [SerializeField]
    Image BlackFadeInOut;
    [SerializeField]
    private Image backgroundComputer;
    [SerializeField]
    private TextMeshProUGUI endText1;
    [SerializeField]
    private GameObject endText2; // �� TMP�� Blink�� �ֱ� ������ Ȱ��ȭ ��Ű����
    [SerializeField]
    private Button endButton;

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
            Invoke("FadeOut", 3f);

            //Test
            Invoke("ChangeImage", 5f);
        }
    }

    public void ChangeImage()
    {
        // Lerp �ʿ�
        backgroundComputer.color = new Color(1, 1, 1, 1);
        endText1.color = new Color(1, 1, 1, 1);
        Invoke("ActivateEndText", 1f);
    }

    public void ActivateEndText()
    {
        endText2.SetActive(true);
    }

    public void ActivateButton()
    {
        endButton.interactable = true;
    }

    public void Fadein()
    {
        volumeController.TriggerFadeIn();
    }

    public void FadeOut()
    {
        volumeController.TriggerFadeOut();
    }
}
