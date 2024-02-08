using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceGiver : MonoBehaviour
{
    // Reinforce 컨테이너 등을 추가하여 (예를 들어 List 형식) 적용된 증강의 넘버를 저장해서
    // 다음에 증강을 호출할 중복된 증강이 호출되지 않도록 방지하는 기능 필요

    [SerializeField]
    private GameObject UI;
    private ReinforceUI reinforceUI;
    List<Dictionary<string, object>> data_dialog;

    public string[] targetStat = new string[3]; // 변화할 타겟 스탯
    public float[] delta = new float[3]; // 스탯의 변화량

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

        // UI 내의 증강 설명 변경
        reinforceUI.text_left.text = data_dialog[numbers[0]]["Description"].ToString();
        reinforceUI.text_middle.text = data_dialog[numbers[1]]["Description"].ToString();
        reinforceUI.text_right.text = data_dialog[numbers[2]]["Description"].ToString();

        // 증강 타겟 스탯 변경
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
