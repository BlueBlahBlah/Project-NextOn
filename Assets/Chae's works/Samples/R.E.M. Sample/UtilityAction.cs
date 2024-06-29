using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAction
{
    public string Name { get; private set; }
    private System.Func<float> calculateUtility; // ��ƿ��Ƽ ��� ��������Ʈ
    private System.Action execute; // �ൿ ���� ��������Ʈ

    public UtilityAction(string name, System.Func<float> calculateUtility, System.Action execute)
    {
        Name = name;
        this.calculateUtility = calculateUtility;
        this.execute = execute;
    }

    public float CalculateUtility()
    {
        return calculateUtility(); // ��ƿ��Ƽ ���
    }

    public void Execute()
    {
        execute(); // �ൿ ����
    }
}
