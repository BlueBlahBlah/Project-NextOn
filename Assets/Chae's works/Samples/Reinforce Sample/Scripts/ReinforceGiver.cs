using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceManager : MonoBehaviour
{
    // Reinforce �����̳� ���� �߰��Ͽ� (���� ��� List ����) ����� ������ �ѹ��� �����ؼ�
    // ������ ������ ȣ���� �ߺ��� ������ ȣ����� �ʵ��� �����ϴ� ��� �ʿ�

    [Header("Reinforce Container")]
    // ȹ���� ������ �ѹ��� �����ϴ� Reinforce Container
    // ���Ե� ������ ������ ������ Weight �� ������ �����ϴ� ������ ����
    [SerializeField]
    public List<int> ReinforceContainer = new List<int>();

    [Header("Rarity (Sum must be 100)")]
    // ���� ����� Ȯ��
    // ����Ƽ inspector���� ������ �� ����
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
    public string[] reinforceName = new string[3]; // ������ �̸� ����
    public int[] reinforceNum = new int[3]; // ������ ��ȣ ����
    public string[] targetStat = new string[3]; // ��ȭ�� Ÿ�� ����
    public float[] delta = new float[3]; // ������ ��ȭ��
    public string[] valueType = new string[3]; // ������ ��ȭ Ÿ��

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
            MixReinforce(); // ������ ���� ���� ���� �̸� �������� UI ����
            OnUI(); // UI Ȱ��ȭ
        }
    }

    public void OnUI()
    {
        UI.SetActive(true);
    }

    private void SelectRarity()
    {
        // ������ ����� Ȯ���� ���� ����
        int[] Rarity = MakeRandomNumbers(0, 100, 1);

        int _rareProbability = normalProbability + rareProbability; 

        // Normal Ȯ��
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
        // csv �����Ϳ� ����� ������ �� �ȿ��� �ߺ����� �ʵ��� �� ����. �Ʒ� �Լ� ����
        int[] numbers = MakeRandomNumbers(0, data_Reinforce.Count, 3);

        // UI ���� ���� �̹��� ����

        // UI ���� ���� �̸� ����

        // UI ���� ���� ���� ����
        reinforceUI.text_left.text = data_Reinforce[numbers[0]]["Description"].ToString();
        reinforceUI.text_middle.text = data_Reinforce[numbers[1]]["Description"].ToString();
        reinforceUI.text_right.text = data_Reinforce[numbers[2]]["Description"].ToString();

        // ���� ��ȣ ����
        reinforceNum[0] = int.Parse(data_Reinforce[numbers[0]]["Number"].ToString());
        reinforceNum[1] = int.Parse(data_Reinforce[numbers[1]]["Number"].ToString());
        reinforceNum[2] = int.Parse(data_Reinforce[numbers[2]]["Number"].ToString());

        // ���� Ÿ�� ���� ����
        targetStat[0] = data_Reinforce[numbers[0]]["TargetStat"].ToString();
        targetStat[1] = data_Reinforce[numbers[1]]["TargetStat"].ToString();
        targetStat[2] = data_Reinforce[numbers[2]]["TargetStat"].ToString();

        // ��ȭ��(value) ����
        delta[0] = float.Parse(data_Reinforce[numbers[0]]["Value"].ToString());
        delta[1] = float.Parse(data_Reinforce[numbers[1]]["Value"].ToString());
        delta[2] = float.Parse(data_Reinforce[numbers[2]]["Value"].ToString());

        // ��ȭ Ÿ�� ���� (Fixed / Percent)
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

        int[] result = new int[number]; // number ���� result ��ȯ
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
