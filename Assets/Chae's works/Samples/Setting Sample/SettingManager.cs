using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("Setting")]
    public RectTransform panel; // ��Ӵٿ� �г��� RectTransform
    public Button showButton; // �г��� ������ ��ư
    public Button hideButton; // �г��� �ø��� ��ư

    private bool isPanelVisible = false; // �г��� ���� ���̴��� ����

    [Header("Dictionary")]
    public Button showDictionaryButton;
    public Button hideDictionaryButton;
    public RectTransform DictionaryPanel;

    public float animationDuration = 0.5f; // �ִϸ��̼� ���� �ð�
    private bool isDictionaryVisible = false;

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ������ ���
        showButton.onClick.AddListener(ShowStatusbar);
        hideButton.onClick.AddListener(HideStatusbar);

        showDictionaryButton.onClick.AddListener(ShowDictionary);
        hideDictionaryButton.onClick.AddListener(HideDictionary);

        // �г��� �ʱ� ��ġ�� �̵� (ȭ�� ���� �����)
        panel.anchoredPosition = new Vector2(0, panel.rect.height);
        panel.gameObject.SetActive(true);

        DictionaryPanel.localScale = Vector3.zero;
    }

    // ����ǥ���� �������� �ִϸ��̼�
    private void ShowStatusbar()
    {
        if (!isPanelVisible)
        {
            // �г� ���̱�
            StartCoroutine(ShowPanelUpToDownAnimation());
        }
    }

    private void HideStatusbar()
    {
        if (isPanelVisible)
        {
            // �г� �����
            StartCoroutine(HidePanelDownToUpAnimation());
        }
    }
    
    private IEnumerator ShowPanelUpToDownAnimation()
    {
        // �г��� ȭ�� ��ܿ��� �������� �ִϸ��̼�
        float elapsedTime = 0f;
        float duration = 0.5f; // �ִϸ��̼� ���� �ð�

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
        // �г��� ȭ�� ������� �ø��� �ִϸ��̼�
        float elapsedTime = 0f;
        float duration = 0.5f; // �ִϸ��̼� ���� �ð�

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

    // Ȯ��Ǿ� ��Ÿ���� �ִϸ��̼�
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

    
}
