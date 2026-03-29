using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalGauge : MonoBehaviour
{
    public Slider slider;
    private bool LastPeizDone = false;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 100;
        
    }

    public void DecreaseGauge_Coriutine()
    {
        StartCoroutine(DecreaseGauge());
    }

    IEnumerator DecreaseGauge()
    {
        float duration = 100f; // Set duration to 100 seconds
        float timer = 0f;
        float initialValue = slider.value;
        float targetValue = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            slider.value = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }

        slider.value = targetValue; // Ensure the value is exactly zero
        MonsterManager.Instance.FinalPeiz = false;      //?´ì œ ëª¬ìŠ¤??ê·¸ë§Œ ?ì„±
        EventManager.Instance.LastPeizDone();           //ëª¨ë“  ëª¬ìŠ¤??ì²˜ì¹˜
        
        if (LastPeizDone == false)
        {
            LastPeizDone = true;
            EventManager.Instance.PrintMSG();               //?¤ìŒ ?€?”ì°½
            EventManager.Instance.CancelBulletSupply();     //??ë³´ì¶© ?„ì´???œëž ê·¸ë§Œ
            EventManager.Instance.CancelDropItem();         //?œë¤ ?¤í‚¬ ?„ì´???œëž ê·¸ë§Œ
            EventManager.Instance.fadeout();                //?”ë©´ ê²€?€?‰ìœ¼ë¡?        }
    }
}
