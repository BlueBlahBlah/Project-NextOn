using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave3 : Gimmick
{
    //3페이즈 90% 넘어갔는지
    private bool Peiz3PersentOver;
    private StageManager stagemanager;
    public Slider slider;

    private void Start()
    {
        Peiz3PersentOver = false;
        slider.value = 0;
        slider = GetComponent<Slider>();
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    public void SetGauge(int h)
    {
        slider.value = h;
    }

    public void StartPeiz3Gauge()
    {
        Debug.LogError("peiz3게이지 시작");
        StartCoroutine(FillTo90());
    }

    IEnumerator FillTo90()
    {
        float duration = 30f; // 30초 동안 진행됩니다.
        float timer = 0f;
        float initialValue = slider.value;
        float targetValue = initialValue + 90f; // 이동할 값은 초기 값에 90을 더합니다.

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            slider.value = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }

        // 30초 이후에는 값이 90이 되지 않고 계속 증가합니다.
        while (true)
        {
            slider.value += Time.deltaTime * 0.1f;
            if (slider.value >= 91)
            {
                slider.value--;
                if (Peiz3PersentOver == false)
                {
                    Peiz3PersentOver = true;
                    //AfterCompilerPannel 패널등장
                    //대형몬스터 등장
                    Debug.LogError("Area3 true");
                    stagemanager.Area3 = true;
                    stagemanager.OnWave3Direction();  //3페이즈 화살표 활성화 + 탈출 벽 비활성화
                    //큰 몬스터 바라보기
                    GameObject.Find("Main Camera").GetComponent<CameraAbove>().LookBigMonster();
                    GameObject.Find("Player").GetComponent<PlayerSpec>().ProtectPlayerWhenBigMonAppear();
                }
            }

            yield return null;
        }
    }
}
