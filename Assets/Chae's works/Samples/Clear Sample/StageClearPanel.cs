using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearPanel : MonoBehaviour
{
    // Scene 변경 관련
    [SerializeField]
    private RectTransform selectPanel;
    public float animationDuration = 0.5f; // 애니메이션 지속 시간

    [SerializeField]
    private VolumeController volumeController;

    // Stage 클리어 판정 관련
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
        
        if (StageClearManager.instance != null) // 만약 StageClearManager 인스턴스가 존재한다면
        {
            // 스테이지 클리어 함수를 불러와 해당 스테이지를 클리어
            StageClearManager.instance.SetStageClear(currentStageNumber, true);
            StageClearManager.instance.isSuccess = true;
        }
        else
        {
            Debug.Log("StageClearManager 가 없습니다!");
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
    

    // 애니메이션 관련 - 선택의 여부 없이 클리어 시 씬을 이동. 닫기 필요 x
    // 패널 열기 애니메이션
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

    private float EaseOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
