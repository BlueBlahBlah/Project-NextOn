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

    private float finalDamage;
    private float finalCritDamage;
    private float finalSkillDamage;
    private float finalSkillCritDamage;

    private void Start()
    {
        Hp.text = MaxHp.ToString();
    }

    public void AttackHit() 
    {
        finalDamage = FinalDamage(PlayerStatManager.instance.ExpectedDamage);
        Hp.text = (float.Parse(Hp.text) - finalDamage).ToString();

        if ((float.Parse(Hp.text) / MaxHp) >= 0)
            Hpbar.fillAmount = float.Parse(Hp.text) / MaxHp;
    }

    public void SkillHit()
    {
        finalDamage = FinalDamage(PlayerStatManager.instance.ExpectedSkillDamage);
        Hp.text = (float.Parse(Hp.text) - finalDamage).ToString();

        if ((float.Parse(Hp.text) / MaxHp) >= 0)
            Hpbar.fillAmount = float.Parse(Hp.text) / MaxHp;
    }


    public float FinalDamage(float _damage)
    {
        float _finalDamage;

        float upperBound = _damage + _damage * (20f/100f); // ���� +20%
        Debug.Log($"upperBound is {upperBound}");
        float lowerBound = _damage - _damage * (20f/100f); // ���� -20%
        Debug.Log($"lowerBound is {lowerBound}");

        _finalDamage = (float)MakeRandomNumbers((int)lowerBound, (int)upperBound + 1, 1)[0];

        Debug.Log($"{_finalDamage}");
        return _finalDamage;
    }

    public void CriticalHit()
    {

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
