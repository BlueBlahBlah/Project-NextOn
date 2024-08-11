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
        float baseUitility = utilityCalculator();
        float noise = Random.Range(-0.1f, 0.1f) * baseUitility;
        return baseUitility + noise; // 유틸리티 계산
    }

    public void Execute()
    {
        execute(); // 행동 실행
    }
}
