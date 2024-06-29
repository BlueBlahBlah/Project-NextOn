using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAction
{
    public string Name { get; private set; }
    private System.Func<float> calculateUtility; // 유틸리티 계산 델리게이트
    private System.Action execute; // 행동 실행 델리게이트

    public UtilityAction(string name, System.Func<float> calculateUtility, System.Action execute)
    {
        Name = name;
        this.calculateUtility = calculateUtility;
        this.execute = execute;
    }

    public float CalculateUtility()
    {
        return calculateUtility(); // 유틸리티 계산
    }

    public void Execute()
    {
        execute(); // 행동 실행
    }
}
