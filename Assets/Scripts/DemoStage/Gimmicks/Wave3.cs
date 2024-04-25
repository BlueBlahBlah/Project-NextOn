using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave3 : Gimmick
{
    //3������ 90% �Ѿ����
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
        Debug.LogError("peiz3������ ����");
        StartCoroutine(FillTo90());
    }

    IEnumerator FillTo90()
    {
        float duration = 30f; // 30�� ���� ����˴ϴ�.
        float timer = 0f;
        float initialValue = slider.value;
        float targetValue = initialValue + 90f; // �̵��� ���� �ʱ� ���� 90�� ���մϴ�.

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            slider.value = Mathf.Lerp(initialValue, targetValue, progress);
            yield return null;
        }

        // 30�� ���Ŀ��� ���� 90�� ���� �ʰ� ��� �����մϴ�.
        while (true)
        {
            slider.value += Time.deltaTime * 0.1f;
            if (slider.value >= 91)
            {
                slider.value--;
                if (Peiz3PersentOver == false)
                {
                    Peiz3PersentOver = true;
                    //AfterCompilerPannel �гε���
                    //�������� ����
                    Debug.LogError("Area3 true");
                    stagemanager.Area3 = true;
                    stagemanager.OnWave3Direction();  //3������ ȭ��ǥ Ȱ��ȭ + Ż�� �� ��Ȱ��ȭ
                    //ū ���� �ٶ󺸱�
                    GameObject.Find("Main Camera").GetComponent<CameraAbove>().LookBigMonster();
                    GameObject.Find("Player").GetComponent<PlayerSpec>().ProtectPlayerWhenBigMonAppear();
                }
            }

            yield return null;
        }
    }
}
