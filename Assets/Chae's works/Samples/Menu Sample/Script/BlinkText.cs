using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;  // TextMeshProUGUI ������Ʈ
    [SerializeField]
    private float initTime = 4f; // ù �ݺ� ���� �ð�
    [SerializeField]
    private float invisibleTime = 3f; // ����ȭ �ֱ�
    [SerializeField]
    private float visibleTime = 1f; // ������ȭ �ֱ�

    private void Start()
    {

        // TextMeshProUGUI ��Ȱ��ȭ
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);

        // 2�� �ڿ� Button ������Ʈ Ȱ��ȭ
        StartCoroutine("LoopCoroutine");
    }

    IEnumerator LoopCoroutine()
    {
        yield return new WaitForSeconds(initTime);
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 1f);
        // TextMeshProUGUI �����̴� ȿ�� �ڷ�ƾ ����
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // 3�� ��� �� TextMeshProUGUI ����
            yield return new WaitForSeconds(invisibleTime);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
            Debug.Log("invisible");

            // 1�� ��� �� TextMeshProUGUI ������
            yield return new WaitForSeconds(visibleTime);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 1f);
            Debug.Log("visible");
        }
    }
}
