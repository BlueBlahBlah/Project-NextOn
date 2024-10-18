using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    

    [Header("Panels")]
    public RectTransform[] panels; // �ִϸ��̼��� ������ �гε��� RectTransform �迭
    public Button[] openButtons; // �г��� ���� ��ư �迭
    public Button[] closeButtons; // �г��� �ݴ� ��ư �迭

    public float animationDuration = 0.5f; // �ִϸ��̼� ���� �ð�

    private void Start()
    {
        

        for (int i = 0; i < openButtons.Length; i++)
        {
            int index = i;
            openButtons[i].onClick.AddListener(() => OpenPanel(index));
            closeButtons[i].onClick.AddListener(() => ClosePanel(index));

            // �г� ũ�� �ʱ�ȭ
            panels[i].localScale = Vector3.zero;
        }

        

    }

    // UI �ִϸ��̼� ���� �Լ�
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

    // UI �ִϸ��̼� ���� �ڷ�ƾ
    #region

    // ����ǥ���� �������� �ִϸ��̼�
    /*
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
    */

    // �г� ���� �ִϸ��̼�
    private IEnumerator OpenPanelCoroutine(int panelIndex)
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        RectTransform panel = panels[panelIndex];
        panel.gameObject.SetActive(true); // �г� Ȱ��ȭ

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, EaseOutQuint(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
    }

    // �г� �ݱ� �ִϸ��̼�
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
        panel.gameObject.SetActive(false); // �г� ��Ȱ��ȭ
    }

    // Ease out quintic function for a smoother animation
    private float EaseOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }

    #endregion

    // UI ��� ���� �Լ�
    private void GoToMenu()
    {

    }
}
