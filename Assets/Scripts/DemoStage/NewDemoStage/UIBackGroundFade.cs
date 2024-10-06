using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackGroundFade : MonoBehaviour
{
    public Image blackImage;  // 캔버스에 있는 Image 컴포넌트
    public float fadeDuration = 1f;  // 페이드가 완료되는 데 걸리는 시간

    public void fadeout()
    {
        StartCoroutine(FadeImageToBlack());
    }
    

    // 이 코루틴을 호출하면 투명한 검은색이 1초에 걸쳐 불투명해집니다.
    private IEnumerator FadeImageToBlack()
    {
        Color color = blackImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackImage.color = color;
            yield return null;
        }
        
        // 마지막으로 알파값을 1로 설정하여 완전히 불투명하게 만듭니다.
        color.a = 1f;
        blackImage.color = color;
    }
}
