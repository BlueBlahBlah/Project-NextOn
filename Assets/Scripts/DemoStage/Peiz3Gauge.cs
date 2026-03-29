using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peiz3Gauge : MonoBehaviour
{
    //3?˜ì´ì¦?90% ?˜ì–´ê°”ëŠ”ì§€
    private bool Peiz3PersentOver;
    public Slider slider;

    private void Start()
    {
        Peiz3PersentOver = false;
        slider.value = 0;
        slider = GetComponent<Slider>();
    }
    
    public void SetGauge(int h)
    {
        slider.value = h;
    }
    
    public void StartPeiz3Gauge()
    {
        Debug.LogError("peiz3ê²Œì´ì§€ ?œì‘");
        StartCoroutine(FillTo90());
    }

    IEnumerator FillTo90()
    {
        float duration = 30f; // 30ì´??™ì•ˆ ì§„í–‰?©ë‹ˆ??
        float timer = 0f;
        float initialValue = slider.value;
        float targetValue = initialValue + 90f; // ?´ë™??ê°’ì? ì´ˆê¸° ê°’ì— 90???”í•©?ˆë‹¤.
    
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            slider.value = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }
    
        // 30ì´??´í›„?ëŠ” ê°’ì´ 90???˜ì? ?Šê³  ê³„ì† ì¦ê??©ë‹ˆ??
        while (true)
        {
            slider.value += Time.deltaTime * 0.1f;
            if (slider.value >= 91)
            {
                slider.value--;
                if (Peiz3PersentOver == false)
                {
                    Peiz3PersentOver = true;
                    //AfterCompilerPannel ?¨ë„?±ì¥
                    //?€?•ëª¬?¤í„° ?±ì¥
                    Debug.LogError("Area3 true");
                    EventManager.Instance.Area3 = true;
                    EventManager.Instance.EscapeCompletion();   //?ˆì¶œ ë²?ë¹„í™œ?±í™”
                    //stagemanager.OnWave3Direction();  //3?˜ì´ì¦??”ì‚´???œì„±??+ ?ˆì¶œ ë²?ë¹„í™œ?±í™”
                    //??ëª¬ìŠ¤??ë°”ë¼ë³´ê¸°
                    //GameObject.Find("Main Camera").GetComponent<CameraAbove>().LookBigMonster();
                    //GameObject.Find("Player").GetComponent<PlayerSpec>().ProtectPlayerWhenBigMonAppear();
                }
            }
            
            yield return null;
        }
    }


}
