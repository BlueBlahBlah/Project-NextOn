using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    

    [Header("Panels")]
    public RectTransform[] panels; // 애니메이션을 적용할 패널들의 RectTransform 배열
    public Button[] openButtons; // 패널을 여는 버튼 배열
    public Button[] closeButtons; // 패널을 닫는 버튼 배열

    public float animationDuration = 0.5f; // 애니메이션 지속 시간

    private void Start()
    {
        

        for (int i = 0; i < openButtons.Length; i++)
        {
            int index = i;
            openButtons[i].onClick.AddListener(() => OpenPanel(index));
            closeButtons[i].onClick.AddListener(() => ClosePanel(index));

            // 패널 크기 초기화
            panels[i].localScale = Vector3.zero;
        }

        

    }

    // UI 애니메이션 관련 함수
    #region
    

    private void OpenPanel(int panelIndex)
    {
        StartCoroutine(OpenPanelCoroutine(panelIndex));
    }

    private void ClosePanel(int panelIndex)
    {
        StartCoroutine(ClosePanelCoroutine(panelIndex));
    }
    #endregion

    // UI 애니메이션 관련 코루틴
    #region

    // 상태표시줄 내려오는 애니메이션
    /*
    private IEnumerator ShowPanelUpToDownAnimation()
    {
        // 패널을 화면 상단에서 내려오는 애니메이션
        float elapsedTime = 0f;
        float duration = 0.5f; // 애니메이션 지속 시간

        Vector2 startPos = new Vector2(0, panel.rect.height);
        Vector2 endPos = Vector2.zero;

        while (elapsedTime < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = endPos;
        isPanelVisible = true;
    }

    private IEnumerator HidePanelDownToUpAnimation()
    {
        // 패널을 화면 상단으로 올리는 애니메이션
        float elapsedTime = 0f;
        float duration = 0.5f; // 애니메이션 지속 시간

        Vector2 startPos = Vector2.zero;
        Vector2 endPos = new Vector2(0, panel.rect.height);

        while (elapsedTime < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = endPos;
        isPanelVisible = false;
    }
    */

    // 패널 열기 애니메이션
    private IEnumerator OpenPanelCoroutine(int panelIndex)
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        RectTransform panel = panels[panelIndex];
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

    // 패널 닫기 애니메이션
    private IEnumerator ClosePanelCoroutine(int panelIndex)
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;
        RectTransform panel = panels[panelIndex];

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
        panel.gameObject.SetActive(false); // 패널 비활성화
    }

    // Ease out quintic function for a smoother animation
    private float EaseOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }

    #endregion

    // UI 기능 관련 함수
    private void GoToMenu()
    {

    }
}
