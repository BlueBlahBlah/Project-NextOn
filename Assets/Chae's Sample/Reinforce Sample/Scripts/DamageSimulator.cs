using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageSimulator : MonoBehaviour
{
    [SerializeField]
    public Image Hpbar;
    [SerializeField]
    public float MaxHp;
    public TextMeshProUGUI Hp;

    private void Start()
    {
        Hp.text = MaxHp.ToString();
    }

    void Update()
    {
        
    }

    public void AttackHit() 
    {
        Hp.text = (float.Parse(Hp.text) - PlayerStatManager.instance.FinalDamage).ToString();

        if ((float.Parse(Hp.text) / MaxHp) >= 0)
            Hpbar.fillAmount = float.Parse(Hp.text) / MaxHp;
    }

    public void SkillHit()
    {
        Hp.text = (float.Parse(Hp.text) - PlayerStatManager.instance.FinalSkillDamage).ToString();

        if ((float.Parse(Hp.text) / MaxHp) >= 0)
            Hpbar.fillAmount = float.Parse(Hp.text) / MaxHp;
    }


    // ũ��Ƽ�� �뵵�� ���� ������. ���� ��Ŀ���򿡵� �������� ���Ǳ� ������ Static ��ũ��Ʈ�� Utils �� �߰��� �ʿ� ����
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

            if (!values.Remove(randomValue))
            {
                break;
            }
        }

        return result;
    }
}
