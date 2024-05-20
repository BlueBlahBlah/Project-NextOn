using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;  // TextMeshProUGUI 컴포넌트
    [SerializeField]
    private float initTime = 4f; // 첫 반복 시작 시간
    [SerializeField]
    private float invisibleTime = 3f; // 투명화 주기
    [SerializeField]
    private float visibleTime = 1f; // 불투명화 주기

    private void Start()
    {

        // TextMeshProUGUI 비활성화
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);

        // 2초 뒤에 Button 컴포넌트 활성화
        StartCoroutine("LoopCoroutine");
    }

    IEnumerator LoopCoroutine()
    {
        yield return new WaitForSeconds(initTime);
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 1f);
        // TextMeshProUGUI 깜빡이는 효과 코루틴 시작
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // 3초 대기 후 TextMeshProUGUI 투명
            yield return new WaitForSeconds(invisibleTime);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
            Debug.Log("invisible");

            // 1초 대기 후 TextMeshProUGUI 불투명
            yield return new WaitForSeconds(visibleTime);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 1f);
            Debug.Log("visible");
        }
    }
}
