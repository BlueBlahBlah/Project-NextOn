using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceGiver : MonoBehaviour
{
    // Reinforce �����̳� ���� �߰��Ͽ� (���� ��� List ����) ����� ������ �ѹ��� �����ؼ�
    // ������ ������ ȣ���� �ߺ��� ������ ȣ����� �ʵ��� �����ϴ� ��� �ʿ�

    [SerializeField]
    private GameObject UI;
    private ReinforceUI reinforceUI;
    List<Dictionary<string, object>> data_dialog;

    public string[] targetStat = new string[3]; // ��ȭ�� Ÿ�� ����
    public float[] delta = new float[3]; // ������ ��ȭ��

    private void Start()
    {
        reinforceUI = UI.GetComponent<ReinforceUI>();
        data_dialog = CSVReader.Read("ReinforceTest");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MixReinforce();
            OnUI();
        }
    }

    public void OnUI()
    {
        UI.SetActive(true);
    }

    private void MixReinforce()
    {
        Debug.Log($"Dialog count : {data_dialog.Count}");

        int[] numbers = MakeRandomNumbers(0, data_dialog.Count);

        // UI ���� ���� ���� ����
        reinforceUI.text_left.text = data_dialog[numbers[0]]["Description"].ToString();
        reinforceUI.text_middle.text = data_dialog[numbers[1]]["Description"].ToString();
        reinforceUI.text_right.text = data_dialog[numbers[2]]["Description"].ToString();

        // ���� Ÿ�� ���� ����
        targetStat[0] = data_dialog[numbers[0]]["TargetStat"].ToString();
        targetStat[1] = data_dialog[numbers[1]]["TargetStat"].ToString();
        targetStat[2] = data_dialog[numbers[2]]["TargetStat"].ToString();

        Debug.Log($"targetstat : {targetStat[0]}");
    }

    private static int[] MakeRandomNumbers(int minValue, int maxValue)
    {
        List<int> values = new List<int>();
        for (int v = minValue; v < maxValue; v++)
        {
            values.Add(v);
        }

        int[] result = new int[3];
        System.Random random = new System.Random();
        for (int i = 0; i < result.Length; i++)
        {
            int randomValue = values[random.Next(0, values.Count)];
            result[i] = randomValue;
            Debug.Log($"randomValue = {randomValue}");

            if(!values.Remove(randomValue))
            {
                break;
            }
        }

        return result;
    }
}
