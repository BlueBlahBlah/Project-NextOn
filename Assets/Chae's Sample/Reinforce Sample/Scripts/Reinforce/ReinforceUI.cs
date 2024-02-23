using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceUI : MonoBehaviour
{
    [SerializeField]
    public GameObject UI;
    [SerializeField]
    private ReinforceManager ReinforceManager;

    [Header("Icon")]
    [SerializeField]
    public Image icon_left;
    [SerializeField]
    public Image icon_middle;
    [SerializeField]
    public Image icon_right;

    [Header("Description")]
    [SerializeField]
    public Text text_left;
    [SerializeField]
    public Text text_middle;
    [SerializeField]
    public Text text_right;

    // 리스크형 증강(x 감소하는 대신, y 크게 증가 등 감소치가 존재)에 대한 아이디어
    // 두 가지 스탯 정보를 호출하기 위해 엑셀 Type에 mixed 라는 값을 추가하고, mixed의 경우 second stat 등을 따로 표기

    #region Reinforce Function
    public void ReinforceStat(int i) // i : 0~2 left, middle, right
    {
        // 0. 이 함수는 '특수 증강'이 아닌 경우에 실행됨.
        // 1. 먼저 ValueType 검사 (Percent / Fixed) -> 미구현
        // 2. 이후 TargetStat 검사
        // 3. 마지막으로 Value를 얻고 증가 -> 미구현

        if (ReinforceManager.valueType[i] == "Fixed")
        {
            // 계산식 : 변경될 스탯 = 기존 스탯 + 변화량
            // 예시 : 기존 스탯 100, 변화량 50
            // 결과 : 100 + 50 = 150
            switch (ReinforceManager.targetStat[i])
            {
                case "health":
                    PlayerStatManager.instance.health = PlayerStatManager.instance.health + ReinforceManager.delta[i];
                    break;
                case "healthRegen":
                    PlayerStatManager.instance.healthRegen = PlayerStatManager.instance.healthRegen + ReinforceManager.delta[i];
                    break;
                case "depense":
                    PlayerStatManager.instance.depense = PlayerStatManager.instance.depense + ReinforceManager.delta[i];
                    break;
                case "speed":
                    PlayerStatManager.instance.speed = PlayerStatManager.instance.speed + ReinforceManager.delta[i];
                    break;
                case "damage":
                    PlayerStatManager.instance.damage = PlayerStatManager.instance.damage + ReinforceManager.delta[i];
                    break;
                case "attackSpeed":
                    PlayerStatManager.instance.attackSpeed = PlayerStatManager.instance.attackSpeed + ReinforceManager.delta[i];
                    break;
                case "skillCooldown":
                    PlayerStatManager.instance.skillCooldown = PlayerStatManager.instance.skillCooldown + ReinforceManager.delta[i];
                    break;
                case "skillDamage":
                    PlayerStatManager.instance.skillDamage = PlayerStatManager.instance.skillDamage + ReinforceManager.delta[i];
                    break;
                case "critProbability":
                    PlayerStatManager.instance.critProbability = PlayerStatManager.instance.critProbability + ReinforceManager.delta[i];
                    break;
                case "critDamage":
                    PlayerStatManager.instance.critDamage = PlayerStatManager.instance.critDamage + ReinforceManager.delta[i];
                    break;
            }
        }
        else if (ReinforceManager.valueType[i] == "Percent")
        {
            // 계산식 => 변경될 스탯 = 기존 스탯 + 기존 스탯*0.01*변화량
            // 예시 : 기존스탯 100, 변화량 5%
            // 결과 : 100 + 100 * 0.01 * 5 = 100 + 5 = 105
            switch (ReinforceManager.targetStat[i])
            {
                case "health":
                    PlayerStatManager.instance.health = PlayerStatManager.instance.health + PlayerStatManager.instance.health * 0.01f * ReinforceManager.delta[i] ;
                    break;
                case "healthRegen":
                    PlayerStatManager.instance.healthRegen = PlayerStatManager.instance.healthRegen + PlayerStatManager.instance.healthRegen * 0.01f * ReinforceManager.delta[i];
                    break;
                case "depense":
                    PlayerStatManager.instance.depense = PlayerStatManager.instance.depense + PlayerStatManager.instance.depense * 0.01f * ReinforceManager.delta[i];
                    break;
                case "speed":
                    PlayerStatManager.instance.speed = PlayerStatManager.instance.speed + PlayerStatManager.instance.speed * 0.01f * ReinforceManager.delta[i];
                    break;
                case "damage":
                    PlayerStatManager.instance.damage = PlayerStatManager.instance.damage + PlayerStatManager.instance.damage * 0.01f * ReinforceManager.delta[i];
                    break;
                case "attackSpeed":
                    PlayerStatManager.instance.attackSpeed = PlayerStatManager.instance.attackSpeed + PlayerStatManager.instance.attackSpeed * 0.01f * ReinforceManager.delta[i];
                    break;
                case "skillCooldown":
                    PlayerStatManager.instance.skillCooldown = PlayerStatManager.instance.skillCooldown + ReinforceManager.delta[i];
                    break;
                case "skillDamage":
                    PlayerStatManager.instance.skillDamage = PlayerStatManager.instance.skillDamage + ReinforceManager.delta[i];
                    break;
                case "critProbability":
                    PlayerStatManager.instance.critProbability = PlayerStatManager.instance.critProbability + ReinforceManager.delta[i];
                    break;
                case "critDamage":
                    PlayerStatManager.instance.critDamage = PlayerStatManager.instance.critDamage + ReinforceManager.delta[i];
                    break;
            }
        }

        PlayerStatManager.instance.UpdateFinalDamage();
        UI.SetActive(false);
    }

    #endregion

    #region OnClick Function
    public void ReinforceLeft()
    {
        // 특수 증강 적용 시, if문으로 먼저 증강 타입 검사
        ReinforceStat(0);
        ReinforceManager.ReinforceContainer.Add(ReinforceManager.reinforceNum[0]);
    }

    public void ReinforceMiddle()
    {
        ReinforceStat(1);
    }

    public void ReinforceRight()
    {
        ReinforceStat(2);
    }
    #endregion
}
