using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceUI : MonoBehaviour
{
    [SerializeField]
    public GameObject UI;
    [SerializeField]
    private ReinforceGiver ReinforceGiver;

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

    #region left Button
    public void ReinforceStat()
    {
        // 0. 이 함수는 '특수 증강'이 아닌 경우에 실행됨.
        // 1. 먼저 ValueType 검사 (Percent / Fixed) -> 미구현
        // 2. 이후 TargetStat 검사
        // 3. 마지막으로 Value를 얻고 증가 -> 미구현

        if (ReinforceGiver.valueType[0] == "Fixed")
        {
            // 계산식 : 변경될 스탯 = 기존 스탯 + 변화량 
            // 예시 : 기존 스탯 100, 변화량 50
            // 결과 : 100 + 50 = 150
            switch (ReinforceGiver.targetStat[0])
            {
                case "health":
                    PlayerStatManager.instance.health = PlayerStatManager.instance.health + ReinforceGiver.delta[0];
                    break;
                case "healthRegen":
                    PlayerStatManager.instance.healthRegen = PlayerStatManager.instance.healthRegen + ReinforceGiver.delta[0];
                    break;
                case "depense":
                    PlayerStatManager.instance.depense = PlayerStatManager.instance.depense + ReinforceGiver.delta[0];
                    break;
                case "speed":
                    PlayerStatManager.instance.speed = PlayerStatManager.instance.speed + ReinforceGiver.delta[0];
                    break;
                case "damage":
                    PlayerStatManager.instance.damage = PlayerStatManager.instance.damage + ReinforceGiver.delta[0];
                    break;
                case "attackSpeed":
                    PlayerStatManager.instance.attackSpeed = PlayerStatManager.instance.attackSpeed + ReinforceGiver.delta[0];
                    break;
                case "skillCooldown":
                    PlayerStatManager.instance.skillCooldown = PlayerStatManager.instance.skillCooldown + ReinforceGiver.delta[0];
                    break;
                case "skillDamage":
                    PlayerStatManager.instance.skillDamage = PlayerStatManager.instance.skillDamage + ReinforceGiver.delta[0];
                    break;
                case "critProbability":
                    PlayerStatManager.instance.critProbability = PlayerStatManager.instance.critProbability + ReinforceGiver.delta[0];
                    break;
                case "critDamage":
                    PlayerStatManager.instance.critDamage = PlayerStatManager.instance.critDamage + ReinforceGiver.delta[0];
                    break;
            }
        }
        else if (ReinforceGiver.valueType[0] == "Percent")
        {
            // 계산식 => 변경될 스탯 = 기존 스탯 + 기존 스탯*0.01*변화량
            // 예시 : 기존스탯 100, 변화량 5%
            // 결과 : 100 + 100 * 0.01 * 5 = 100 + 5 = 105
            switch (ReinforceGiver.targetStat[0])
            {
                case "health":
                    PlayerStatManager.instance.health = PlayerStatManager.instance.health + PlayerStatManager.instance.health * 0.01f * ReinforceGiver.delta[0] ;
                    break;
                case "healthRegen":
                    PlayerStatManager.instance.healthRegen = PlayerStatManager.instance.healthRegen + PlayerStatManager.instance.healthRegen * 0.01f * ReinforceGiver.delta[0];
                    break;
                case "depense":
                    PlayerStatManager.instance.depense = PlayerStatManager.instance.depense + PlayerStatManager.instance.depense * 0.01f * ReinforceGiver.delta[0];
                    break;
                case "speed":
                    PlayerStatManager.instance.speed = PlayerStatManager.instance.speed + PlayerStatManager.instance.speed * 0.01f * ReinforceGiver.delta[0];
                    break;
                case "damage":
                    PlayerStatManager.instance.damage = PlayerStatManager.instance.damage + PlayerStatManager.instance.damage * 0.01f * ReinforceGiver.delta[0];
                    break;
                case "attackSpeed":
                    PlayerStatManager.instance.attackSpeed = PlayerStatManager.instance.attackSpeed + PlayerStatManager.instance.attackSpeed * 0.01f * ReinforceGiver.delta[0];
                    break;
                case "skillCooldown":
                    PlayerStatManager.instance.skillCooldown = PlayerStatManager.instance.skillCooldown + ReinforceGiver.delta[0];
                    break;
                case "skillDamage":
                    PlayerStatManager.instance.skillDamage = PlayerStatManager.instance.skillDamage + ReinforceGiver.delta[0];
                    break;
                case "critProbability":
                    PlayerStatManager.instance.critProbability = PlayerStatManager.instance.critProbability + ReinforceGiver.delta[0];
                    break;
                case "critDamage":
                    PlayerStatManager.instance.critDamage = PlayerStatManager.instance.critDamage + ReinforceGiver.delta[0];
                    break;
            }
        }
        
        
        UI.SetActive(false);
    }

    #endregion
}
