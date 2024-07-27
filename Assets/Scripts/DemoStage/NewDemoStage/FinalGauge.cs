using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalGauge : MonoBehaviour
{
    public Slider slider;

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
        MonsterManager.Instance.FinalPeiz = false;
    }
}