using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    public RectTransform panel; // 드롭다운 패널의 RectTransform
    public Button showButton; // 패널을 내리는 버튼
    public Button hideButton; // 패널을 올리는 버튼

    private bool isPanelVisible = false; // 패널이 현재 보이는지 여부

    private void Start()
    {
        // 버튼 클릭 이벤트 리스너 등록
        showButton.onClick.AddListener(ShowPanel);
        hideButton.onClick.AddListener(HidePanel);

        // 패널을 초기 위치로 이동 (화면 위로 숨기기)
        panel.anchoredPosition = new Vector2(0, panel.rect.height);
    }

    private void ShowPanel()
    {
        if (!isPanelVisible)
        {
            // 패널 보이기
            StartCoroutine(ShowPanelAnimation());
        }
    }

    private void HidePanel()
    {
        if (isPanelVisible)
        {
            // 패널 숨기기
            StartCoroutine(HidePanelAnimation());
        }
    }

    private IEnumerator ShowPanelAnimation()
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

    private IEnumerator HidePanelAnimation()
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
}
