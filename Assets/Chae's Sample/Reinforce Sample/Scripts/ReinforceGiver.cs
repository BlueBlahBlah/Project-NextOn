using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceManager : MonoBehaviour
{
    // Reinforce 컨테이너 등을 추가하여 (예를 들어 List 형식) 적용된 증강의 넘버를 저장해서
    // 다음에 증강을 호출할 중복된 증강이 호출되지 않도록 방지하는 기능 필요

    [Header("Reinforce Container")]
    // 획득한 증강의 넘버를 저장하는 Reinforce Container
    // 포함된 증강의 정보를 가지고 Weight 등 변수를 조절하는 역할을 수행
    [SerializeField]
    public List<int> ReinforceContainer = new List<int>();

    [Header("Rarity (Sum must be 100)")]
    // 증강 등급의 확률
    // 유니티 inspector에서 수정할 수 있음
    [SerializeField]
    private int normalProbability;
    [SerializeField]
    private int rareProbability;
    [SerializeField]
    private int epicProbability;

    [Header("UI")]
    [SerializeField]
    private GameObject UI;
    private ReinforceUI reinforceUI;
    List<Dictionary<string, object>> data_Reinforce;

    [Header("Data")]
    public string[] reinforceName = new string[3]; // 증강의 이름 저장
    public int[] reinforceNum = new int[3]; // 증강의 번호 저장
    public string[] targetStat = new string[3]; // 변화할 타겟 스탯
    public float[] delta = new float[3]; // 스탯의 변화량
    public string[] valueType = new string[3]; // 스탯의 변화 타입

    private void Start()
    {
        reinforceUI = UI.GetComponent<ReinforceUI>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectRarity();
            MixReinforce(); // 랜덤한 증강 정보 얻어와 이를 바탕으로 UI 변경
            OnUI(); // UI 활성화
        }
    }

    public void OnUI()
    {
        UI.SetActive(true);
    }

    private void SelectRarity()
    {
        // 증강의 등급을 확률에 의해 선택
        int[] Rarity = MakeRandomNumbers(0, 100, 1);

        int _rareProbability = normalProbability + rareProbability; 

        // Normal 확률
        if (Rarity[0] < normalProbability)
        {
            Debug.Log("normal reinforce");
            data_Reinforce = CSVReader.Read("Data (.csv)/Normal Reinforce");
        }
        else if (Rarity[0] < _rareProbability)
        {
            Debug.Log("rare reinforce");
            data_Reinforce = CSVReader.Read("Data (.csv)/Rare Reinforce");
        }
        else
        {
            Debug.Log("epic reinforce");
            data_Reinforce = CSVReader.Read("Data (.csv)/Epic Reinforce");
        }
    }

    private void MixReinforce()
    {
        // csv 데이터에 저장된 증강의 수 안에서 중복되지 않도록 값 생성. 아래 함수 참조
        int[] numbers = MakeRandomNumbers(0, data_Reinforce.Count, 3);

        // UI 내의 증강 이미지 변경

        // UI 내의 증강 이름 변경

        // UI 내의 증강 설명 변경
        reinforceUI.text_left.text = data_Reinforce[numbers[0]]["Description"].ToString();
        reinforceUI.text_middle.text = data_Reinforce[numbers[1]]["Description"].ToString();
        reinforceUI.text_right.text = data_Reinforce[numbers[2]]["Description"].ToString();

        // 증강 번호 저장
        reinforceNum[0] = int.Parse(data_Reinforce[numbers[0]]["Number"].ToString());
        reinforceNum[1] = int.Parse(data_Reinforce[numbers[1]]["Number"].ToString());
        reinforceNum[2] = int.Parse(data_Reinforce[numbers[2]]["Number"].ToString());

        // 증강 타겟 스탯 저장
        targetStat[0] = data_Reinforce[numbers[0]]["TargetStat"].ToString();
        targetStat[1] = data_Reinforce[numbers[1]]["TargetStat"].ToString();
        targetStat[2] = data_Reinforce[numbers[2]]["TargetStat"].ToString();

        // 변화량(value) 저장
        delta[0] = float.Parse(data_Reinforce[numbers[0]]["Value"].ToString());
        delta[1] = float.Parse(data_Reinforce[numbers[1]]["Value"].ToString());
        delta[2] = float.Parse(data_Reinforce[numbers[2]]["Value"].ToString());

        // 변화 타입 저장 (Fixed / Percent)
        valueType[0] = data_Reinforce[numbers[0]]["ValueType"].ToString();
        valueType[1] = data_Reinforce[numbers[1]]["ValueType"].ToString();
        valueType[2] = data_Reinforce[numbers[2]]["ValueType"].ToString();
    }

    private int[] MakeRandomNumbers(int minValue, int maxValue, int number)
    {
        List<int> values = new List<int>();
        for (int v = minValue; v < maxValue; v++)
        {
            values.Add(v);
        }

        int[] result = new int[number]; // number 개의 result 반환
        System.Random random = new System.Random();
        for (int i = 0; i < result.Length; i++)
        {
            int randomValue = values[random.Next(0, values.Count)];
            result[i] = randomValue;

            if(!values.Remove(randomValue))
            {
                break;
            }
        }

        return result;
    }
}
