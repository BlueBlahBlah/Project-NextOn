using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Reinforce �����̳ʸ� �߰��Ͽ� (���� ��� List ����) ����� ������ �ѹ��� �����ؼ�
    // ������ ������ ȣ���� �ߺ��� ������ ȣ����� �ʵ��� �����ϴ� ��� �ʿ�

    [SerializeField]
    private GameObject UI;
    private ReinforceUI reinforceUI;
    List<Dictionary<string, object>> data_dialog;

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

        reinforceUI.text_left.text = data_dialog[numbers[0]]["Description"].ToString();
        reinforceUI.text_middle.text = data_dialog[numbers[1]]["Description"].ToString();
        reinforceUI.text_right.text = data_dialog[numbers[2]]["Description"].ToString();
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
