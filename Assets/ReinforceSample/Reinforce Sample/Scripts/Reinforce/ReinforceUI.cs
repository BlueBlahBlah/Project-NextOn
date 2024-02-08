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

        switch (ReinforceGiver.targetStat[0])
        {
            case "health":
                PlayerStatManager.instance.health = PlayerStatManager.instance.health + 2;
                break;
            case "healthRegen":
                PlayerStatManager.instance.healthRegen = PlayerStatManager.instance.healthRegen + 2;
                break;
            case "depense":
                PlayerStatManager.instance.depense = PlayerStatManager.instance.depense + 2;
                break;
            case "speed":
                PlayerStatManager.instance.speed = PlayerStatManager.instance.speed + 2;
                break;
            case "damage":
                PlayerStatManager.instance.damage = PlayerStatManager.instance.damage + 2;
                break;
            case "attackSpeed":
                PlayerStatManager.instance.attackSpeed = PlayerStatManager.instance.attackSpeed + 2;
                break;
            case "skillCooldown":
                PlayerStatManager.instance.skillCooldown = PlayerStatManager.instance.skillCooldown + 2;
                break;
            case "skillDamage":
                PlayerStatManager.instance.skillDamage = PlayerStatManager.instance.skillDamage + 2;
                break;
            case "critProbability":
                PlayerStatManager.instance.critProbability = PlayerStatManager.instance.critProbability + 2;
                break;
            case "critDamage":
                PlayerStatManager.instance.critDamage = PlayerStatManager.instance.critDamage + 2;
                break;
        }
        
        UI.SetActive(false);
    }

    #endregion
}
