using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peiz3Gauge : MonoBehaviour
{
    //3?섏씠利?90% ?섏뼱媛붾뒗吏
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
        Debug.LogError("peiz3寃뚯씠吏 ?쒖옉");
        StartCoroutine(FillTo90());
    }

    IEnumerator FillTo90()
    {
        float duration = 30f; // 30珥??숈븞 吏꾪뻾?⑸땲??
        float timer = 0f;
        float initialValue = slider.value;
        float targetValue = initialValue + 90f; // ?대룞??媛믪? 珥덇린 媛믪뿉 90???뷀빀?덈떎.
    
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            slider.value = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }
    
        // 30珥??댄썑?먮뒗 媛믪씠 90???섏? ?딄퀬 怨꾩냽 利앷??⑸땲??
        while (true)
        {
            slider.value += Time.deltaTime * 0.1f;
            if (slider.value >= 91)
            {
                slider.value--;
                if (Peiz3PersentOver == false)
                {
                    Peiz3PersentOver = true;
                    //AfterCompilerPannel ?⑤꼸?깆옣
                    //??뺣が?ㅽ꽣 ?깆옣
                    Debug.LogError("Area3 true");
                    EventManager.Instance.Area3 = true;
                    EventManager.Instance.EscapeCompletion();   //?덉텧 踰?鍮꾪솢?깊솕
                    //stagemanager.OnWave3Direction();  //3?섏씠利??붿궡???쒖꽦??+ ?덉텧 踰?鍮꾪솢?깊솕
                    //??紐ъ뒪??諛붾씪蹂닿린
                    //GameObject.Find("Main Camera").GetComponent<CameraAbove>().LookBigMonster();
                    //GameObject.Find("Player").GetComponent<PlayerSpec>().ProtectPlayerWhenBigMonAppear();
                }
            }
            
            yield return null;
        }
    }


}
