using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    public Button button;  // Button ������Ʈ
    public TextMeshProUGUI textMeshPro;  // TextMeshProUGUI ������Ʈ

    private void Start()
    {
        // Button ������Ʈ ��Ȱ��ȭ
        button.interactable = false;

        // TextMeshProUGUI ��Ȱ��ȭ
        textMeshPro.gameObject.SetActive(false);

        // 2�� �ڿ� Button ������Ʈ Ȱ��ȭ
        StartCoroutine(ActivateButton());
    }

    IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(4f);

        // Button ������Ʈ Ȱ��ȭ
        button.interactable = true;

        // TextMeshProUGUI Ȱ��ȭ
        textMeshPro.gameObject.SetActive(true);

        // TextMeshProUGUI �����̴� ȿ�� �ڷ�ƾ ����
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            // 1�� ��� �� TextMeshProUGUI ��Ȱ��ȭ
            yield return new WaitForSeconds(3f);
            textMeshPro.gameObject.SetActive(false);

            // 1�� ��� �� TextMeshProUGUI Ȱ��ȭ
            yield return new WaitForSeconds(1f);
            textMeshPro.gameObject.SetActive(true);
        }
    }
}
