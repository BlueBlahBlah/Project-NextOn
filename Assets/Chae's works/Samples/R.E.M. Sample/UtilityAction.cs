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
        float baseUitility = utilityCalculator();
        float noise = Random.Range(-0.1f, 0.1f) * baseUitility;
        return baseUitility + noise; // ��ƿ��Ƽ ���
    }

    public void Execute()
    {
        execute(); // �ൿ ����
    }
}
