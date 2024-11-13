using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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


    private float timer = 0f;         // �ð� ������ ����
    private Color bgColor;            // backgroundComputer�� Color ����
    private Color textColor;

    private bool lerpAlpha = false;
    private float elapsedTime = 0f;
    public float duration = 3f;

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
            // Invoke("StartLerpAlpha", 5f);
        }
    }

    private void Update()
    {
        if (lerpAlpha)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);  // 0���� 1 ���� �� ���

            // Lerp�� ����Ͽ� alpha ���� �ε巴�� 1�� ����
            Color bgColor = backgroundComputer.color;
            bgColor.a = Mathf.Lerp(0, 1, t);
            backgroundComputer.color = bgColor;

            Color textColor = endText1.color;
            textColor.a = Mathf.Lerp(0, 1, t);
            endText1.color = textColor;

            if (t >= 1f)
            {
                lerpAlpha = false;  // �Ϸ� �� Lerp ��Ȱ��ȭ
            }
        }
    }

    public void StartLerpAlpha()
    {
        lerpAlpha = true;
        elapsedTime = 0f;  // Ÿ�̸� �ʱ�ȭ
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
