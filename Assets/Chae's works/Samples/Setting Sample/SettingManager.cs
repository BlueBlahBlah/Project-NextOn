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
        StartCoroutine(OpenPanelCoroutine(panelIndex, openButtons[panelIndex].transform as RectTransform));
    }

    private void ClosePanel(int panelIndex)
    {
        StartCoroutine(ClosePanelCoroutine(panelIndex, openButtons[panelIndex].transform as RectTransform));
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
    private IEnumerator OpenPanelCoroutine(int panelIndex, RectTransform buttonRectTransform)
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        RectTransform panel = panels[panelIndex];
        panel.gameObject.SetActive(true);

        // 월드 좌표에서 패널 로컬 좌표로 변환
        Vector2 buttonPivot = GetButtonPivotInPanel(buttonRectTransform, panel);

        // 원래 피벗을 저장하고 새로운 피벗 설정
        Vector2 originalPivot = panel.pivot;
        panel.pivot = buttonPivot;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
        panel.pivot = originalPivot;
    }

    // 패널 닫기 애니메이션
    private IEnumerator ClosePanelCoroutine(int panelIndex, RectTransform buttonRectTransform)
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;
        RectTransform panel = panels[panelIndex];

        // 월드 좌표에서 패널 로컬 좌표로 변환
        Vector2 buttonPivot = GetButtonPivotInPanel(buttonRectTransform, panel);

        // 원래 피벗을 저장하고 새로운 피벗 설정
        Vector2 originalPivot = panel.pivot;
        panel.pivot = buttonPivot;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
        panel.pivot = originalPivot;
        panel.gameObject.SetActive(false); // 패널 비활성화
    }

    // 버튼의 피벗을 패널 로컬 좌표로 변환
    private Vector2 GetButtonPivotInPanel(RectTransform buttonRectTransform, RectTransform panelRectTransform)
    {
        Vector3[] buttonWorldCorners = new Vector3[4];
        buttonRectTransform.GetWorldCorners(buttonWorldCorners);

        // 버튼의 중앙점 계산
        Vector3 buttonCenterWorld = (buttonWorldCorners[0] + buttonWorldCorners[2]) / 2;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, buttonCenterWorld, null, out localPoint);

        // 로컬 좌표를 피벗 비율로 변환
        return new Vector2(localPoint.x / panelRectTransform.rect.width + 0.5f, localPoint.y / panelRectTransform.rect.height + 0.5f);
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
