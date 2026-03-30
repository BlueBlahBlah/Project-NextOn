using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackGroundFade : MonoBehaviour
{
    public Image blackImage;  // 罹붾쾭?ㅼ뿉 ?덈뒗 Image 而댄룷?뚰듃
    public float fadeDuration = 1f;  // ?섏씠?쒓? ?꾨즺?섎뒗 ??嫄몃━???쒓컙

    public void fadeout()
    {
        StartCoroutine(FadeImageToBlack());
    }
    

    // ??肄붾（?댁쓣 ?몄텧?섎㈃ ?щ챸??寃??됱씠 1珥덉뿉 嫄몄퀜 遺덊닾紐낇빐吏묐땲??
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
        
        // 留덉?留됱쑝濡??뚰뙆媛믪쓣 1濡??ㅼ젙?섏뿬 ?꾩쟾??遺덊닾紐낇븯寃?留뚮벊?덈떎.
        color.a = 1f;
        blackImage.color = color;
    }
}
