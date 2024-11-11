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
    private GameObject endText2; // 이 TMP는 Blink가 있기 때문에 활성화 시키도록
    [SerializeField]
    private Button endButton;

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
            Invoke("FadeOut", 3f);

            //Test
            Invoke("ChangeImage", 5f);
        }
    }

    public void ChangeImage()
    {
        // Lerp 필요
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
