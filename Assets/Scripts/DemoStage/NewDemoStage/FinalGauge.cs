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
        float duration = 100f; 
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

        slider.value = targetValue; 
        MonsterManager.Instance.FinalPeiz = false;      
        EventManager.Instance.LastPeizDone();           
        
        if (LastPeizDone == false)
        {
            LastPeizDone = true;
            EventManager.Instance.PrintMSG();               
            EventManager.Instance.CancelBulletSupply();     
            EventManager.Instance.CancelDropItem();         
            EventManager.Instance.fadeout();                
        }
    }
}
