using System.Collections;
using System.Collections.Generic;
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
    public GameObject Dictionary;

    private bool isDictionaryVisible = false;

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ������ ���
        showButton.onClick.AddListener(ShowPanel);
        hideButton.onClick.AddListener(HidePanel);

        showDictionaryButton.onClick.AddListener(ShowDictionary);
        hideDictionaryButton.onClick.AddListener(HideDictionary);

        // �г��� �ʱ� ��ġ�� �̵� (ȭ�� ���� �����)
        panel.anchoredPosition = new Vector2(0, panel.rect.height);
        panel.gameObject.SetActive(true);
    }

    private void ShowPanel()
    {
        if (!isPanelVisible)
        {
            // �г� ���̱�
            StartCoroutine(ShowPanelAnimation());
        }
    }

    private void HidePanel()
    {
        if (isPanelVisible)
        {
            // �г� �����
            StartCoroutine(HidePanelAnimation());
        }
    }

    private IEnumerator ShowPanelAnimation()
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

    private IEnumerator HidePanelAnimation()
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
            Dictionary.SetActive(true);
            isDictionaryVisible = true;
        }
    }

    private void HideDictionary()
    {
        if (isDictionaryVisible)
        {
            Dictionary.SetActive(false);
            isDictionaryVisible = false;
        }
    }
}
