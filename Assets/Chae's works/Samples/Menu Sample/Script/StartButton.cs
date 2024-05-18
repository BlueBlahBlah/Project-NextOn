using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    public Button button;  // Button 컴포넌트
    public TextMeshProUGUI textMeshPro;  // TextMeshProUGUI 컴포넌트

    private void Start()
    {
        // Button 컴포넌트 비활성화
        button.interactable = false;

        // TextMeshProUGUI 비활성화
        textMeshPro.gameObject.SetActive(false);

        // 2초 뒤에 Button 컴포넌트 활성화
        StartCoroutine(ActivateButton());
    }

    IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(4f);

        // Button 컴포넌트 활성화
        button.interactable = true;

        // TextMeshProUGUI 활성화
        textMeshPro.gameObject.SetActive(true);

        // TextMeshProUGUI 깜빡이는 효과 코루틴 시작
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            // 1초 대기 후 TextMeshProUGUI 비활성화
            yield return new WaitForSeconds(3f);
            textMeshPro.gameObject.SetActive(false);

            // 1초 대기 후 TextMeshProUGUI 활성화
            yield return new WaitForSeconds(1f);
            textMeshPro.gameObject.SetActive(true);
        }
    }
}
