using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UtilityAction
{
    public string Name { get; private set; }
    private System.Func<float> utilityCalculator; // ��ƿ��Ƽ ��� ��������Ʈ
    private System.Action execute; // �ൿ ���� ��������Ʈ

    public UtilityAction(string name, System.Func<float> calculateUtility, System.Action execute)
    {
        Name = name;
        this.utilityCalculator = calculateUtility;
        this.execute = execute;
    }

    public float CalculateUtility()
    {
        float baseUtility = utilityCalculator();

        // ������ ���� ����
        float minNoise = 0.5f;
        float maxNoise = 5.0f;

        // ������ ���� ���� ���� �� ������ ���� ���
        float noise = 0f;
        if (baseUtility >= 10f && baseUtility <= 90f)
        {
            float normalizedUtility = (baseUtility - 10f) / 80f; // 10~90 ���� ����ȭ
            float noiseStrength = Mathf.Lerp(minNoise, maxNoise, normalizedUtility);
            noise = Random.Range(-noiseStrength, noiseStrength);
        }

        Debug.Log(baseUtility + noise);
        return baseUtility + noise; // ��ƿ��Ƽ ���
    }

    public void Execute()
    {
        execute(); // �ൿ ����
    }
}
