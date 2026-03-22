using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public float progressPercent = 0;

    public Slider slider;

    private void Start()
    {
        slider.value = 0;
        slider = GetComponent<Slider>();
    }

    public void SetHealth(int h)
    {
        slider.value = h;
    }
    
    public void ClearWave2()
    {
        StartCoroutine(DecreaseGauge());
    }

    IEnumerator DecreaseGauge()
    {
        float duration = 3f;
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
    }
    
}
