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

    // ����ũ�� ����(x �����ϴ� ���, y ũ�� ���� �� ����ġ�� ����)�� ���� ���̵��
    // �� ���� ���� ������ ȣ���ϱ� ���� ���� Type�� mixed ��� ���� �߰��ϰ�, mixed�� ��� second stat ���� ���� ǥ��

    #region left Button
    public void ReinforceStat()
    {
        // 0. �� �Լ��� 'Ư�� ����'�� �ƴ� ��쿡 �����.
        // 1. ���� ValueType �˻� (Percent / Fixed) -> �̱���
        // 2. ���� TargetStat �˻�
        // 3. ���������� Value�� ��� ���� -> �̱���

        if (ReinforceGiver.valueType[0] == "Fixed")
        {
            // ���� : ����� ���� = ���� ���� + ��ȭ�� 
            // ���� : ���� ���� 100, ��ȭ�� 50
            // ��� : 100 + 50 = 150
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
            // ���� => ����� ���� = ���� ���� + ���� ����*0.01*��ȭ��
            // ���� : �������� 100, ��ȭ�� 5%
            // ��� : 100 + 100 * 0.01 * 5 = 100 + 5 = 105
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
