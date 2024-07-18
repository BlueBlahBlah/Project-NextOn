using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("Setting")]
    public RectTransform panel; // 드롭다운 패널의 RectTransform
    public Button showButton; // 패널을 내리는 버튼
    public Button hideButton; // 패널을 올리는 버튼

    private bool isPanelVisible = false; // 패널이 현재 보이는지 여부

    [Header("Dictionary")]
    public Button showDictionaryButton;
    public Button hideDictionaryButton;
    public RectTransform DictionaryPanel;

    public float animationDuration = 0.5f; // 애니메이션 지속 시간
    private bool isDictionaryVisible = false;

    [Header("Sound")]
    public Button showSoundButton;
    public RectTransform soundPanel;

    [Header("REM")]
    public Button showREMButton;

    [Header("Item")]
    public Button showItemButton;

    [Header("Quit")]
    public Button showQuitButton;

    private void Start()
    {
        // 버튼 클릭 이벤트 리스너 등록
        showButton.onClick.AddListener(ShowStatusbar);
        hideButton.onClick.AddListener(HideStatusbar);

        showDictionaryButton.onClick.AddListener(ShowDictionary);
        hideDictionaryButton.onClick.AddListener(HideDictionary);

        // 패널을 초기 위치로 이동 (화면 위로 숨기기)
        panel.anchoredPosition = new Vector2(0, panel.rect.height);
        panel.gameObject.SetActive(true);

        DictionaryPanel.localScale = Vector3.zero;
        DictionaryPanel.gameObject.SetActive(true);
    }

   
    private void ShowStatusbar()
    {
        if (!isPanelVisible)
        {
            // 패널 보이기
            StartCoroutine(ShowPanelUpToDownAnimation());
        }
    }

    private void HideStatusbar()
    {
        if (isPanelVisible)
        {
            // 패널 숨기기
            StartCoroutine(HidePanelDownToUpAnimation());
        }
    }

    private void ShowDictionary()
    {
        if (!isDictionaryVisible)
        {
            StartCoroutine(ShowPanelGrowAnimation());
        }
    }

    private void HideDictionary()
    {
        if (isDictionaryVisible)
        {
            StartCoroutine(ClosePanelDownsizeAnimation());
        }
    }

    private void ShowSoundButton()
    {

    }
    
    public void HideSoundButton()
    {

    }

    private void ShowREMButton()
    {

    }

    private void HideREMButton()
    {

    }

    private void ShowItemButton()
    {

    }

    private void HideItemButton()
    {

    }

    private void ShowQuitButton()
    {

    }

    private void HideQuitButton()
    {

    }

    // 애니메이션 관련
    #region

    // 상태표시줄 내려오는 애니메이션
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

    // 확장되어 나타나는 애니메이션
    private IEnumerator ShowPanelGrowAnimation()
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsedTime < animationDuration)
        {
            DictionaryPanel.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        DictionaryPanel.localScale = endScale;
        isDictionaryVisible = true;
    }

    private IEnumerator ClosePanelDownsizeAnimation()
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;

        while (elapsedTime < animationDuration)
        {
            DictionaryPanel.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        DictionaryPanel.localScale = endScale;
        isDictionaryVisible = false;
    }

    #endregion
}
