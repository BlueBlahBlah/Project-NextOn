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
    private GameObject endText2; // 이 TMP는 Blink가 있기 때문에 활성화 시키도록
    [SerializeField]
    private Button endButton;

    private VolumeController volumeController;


    private float timer = 0f;         // 시간 추적용 변수
    private Color bgColor;            // backgroundComputer의 Color 저장
    private Color textColor;

    private bool lerpAlpha = false;
    private float elapsedTime = 0f;
    public float duration = 3f;

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
            // Invoke("StartLerpAlpha", 5f);
        }
    }

    private void Update()
    {
        if (lerpAlpha)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);  // 0에서 1 사이 값 계산

            // Lerp를 사용하여 alpha 값을 부드럽게 1로 변경
            Color bgColor = backgroundComputer.color;
            bgColor.a = Mathf.Lerp(0, 1, t);
            backgroundComputer.color = bgColor;

            Color textColor = endText1.color;
            textColor.a = Mathf.Lerp(0, 1, t);
            endText1.color = textColor;

            if (t >= 1f)
            {
                lerpAlpha = false;  // 완료 후 Lerp 비활성화
            }
        }
    }

    public void StartLerpAlpha()
    {
        lerpAlpha = true;
        elapsedTime = 0f;  // 타이머 초기화
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
