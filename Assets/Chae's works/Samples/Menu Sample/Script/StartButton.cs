using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    public Button button;  // Button 컴포넌트

    private void Start()
    {
        // Button 컴포넌트 비활성화
        button.interactable = false;

        // 2초 뒤에 Button 컴포넌트 활성화
        StartCoroutine(ActivateButton());
    }

    IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(4f);

        // Button 컴포넌트 활성화
        button.interactable = true;
    }
}
