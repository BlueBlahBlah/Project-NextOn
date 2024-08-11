using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    public Button button;  // Button ������Ʈ

    private void Start()
    {
        // Button ������Ʈ ��Ȱ��ȭ
        button.interactable = false;

        // 2�� �ڿ� Button ������Ʈ Ȱ��ȭ
        StartCoroutine(ActivateButton());
    }

    IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(4f);

        // Button ������Ʈ Ȱ��ȭ
        button.interactable = true;
    }
}
