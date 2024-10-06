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
        MonsterManager.Instance.FinalPeiz = false;      //이제 몬스터 그만 생성
        EventManager.Instance.LastPeizDone();           //모든 몬스터 처치
        
        if (LastPeizDone == false)
        {
            LastPeizDone = true;
            EventManager.Instance.PrintMSG();               //다음 대화창
            EventManager.Instance.CancelBulletSupply();     //탄 보충 아이템 드랍 그만
            EventManager.Instance.CancelDropItem();         //랜덤 스킬 아이템 드랍 그만
            EventManager.Instance.fadeout();                //화면 검은색으로
        }
    }
}