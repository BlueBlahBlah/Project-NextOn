using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackGroundFade : MonoBehaviour
{
    public Image blackImage;  // ìº”ë²„?¤ì— ?ˆëŠ” Image ì»´í¬?ŒíŠ¸
    public float fadeDuration = 1f;  // ?˜ì´?œê? ?„ë£Œ?˜ëŠ” ??ê±¸ë¦¬???œê°„

    public void fadeout()
    {
        StartCoroutine(FadeImageToBlack());
    }
    

    // ??ì½”ë£¨?´ì„ ?¸ì¶œ?˜ë©´ ?¬ëª…??ê²€?€?‰ì´ 1ì´ˆì— ê±¸ì³ ë¶ˆíˆ¬ëª…í•´ì§‘ë‹ˆ??
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
        
        // ë§ˆì?ë§‰ìœ¼ë¡??ŒíŒŒê°’ì„ 1ë¡??¤ì •?˜ì—¬ ?„ì „??ë¶ˆíˆ¬ëª…í•˜ê²?ë§Œë“­?ˆë‹¤.
        color.a = 1f;
        blackImage.color = color;
    }
}
