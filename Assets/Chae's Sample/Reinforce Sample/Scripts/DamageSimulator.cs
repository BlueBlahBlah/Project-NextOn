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


    // 크리티컬 용도의 난수 생성기. 증강 매커니즘에도 공용으로 사용되기 때문에 Static 스크립트인 Utils 를 추가할 필요 있음
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

            if (!values.Remove(randomValue))
            {
                break;
            }
        }

        return result;
    }
}
