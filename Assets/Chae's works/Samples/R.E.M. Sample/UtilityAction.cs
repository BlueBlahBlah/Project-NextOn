using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UtilityAction
{
    public string Name { get; private set; }
    private System.Func<float> utilityCalculator; // 유틸리티 계산 델리게이트
    private System.Action execute; // 행동 실행 델리게이트

    public UtilityAction(string name, System.Func<float> calculateUtility, System.Action execute)
    {
        Name = name;
        this.utilityCalculator = calculateUtility;
        this.execute = execute;
    }

    public float CalculateUtility()
    {
        float baseUtility = utilityCalculator();

        // 노이즈 범위 설정
        float minNoise = 0.5f;
        float maxNoise = 5.0f;

        // 노이즈 적용 여부 결정 및 노이즈 강도 계산
        float noise = 0f;
        if (baseUtility >= 10f && baseUtility <= 90f)
        {
            float normalizedUtility = (baseUtility - 10f) / 80f; // 10~90 범위 정규화
            float noiseStrength = Mathf.Lerp(minNoise, maxNoise, normalizedUtility);
            noise = Random.Range(-noiseStrength, noiseStrength);
        }

        Debug.Log(baseUtility + noise);
        return baseUtility + noise; // 유틸리티 계산
    }

    public void Execute()
    {
        execute(); // 행동 실행
    }
}
