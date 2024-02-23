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

    // ����ũ�� ����(x �����ϴ� ���, y ũ�� ���� �� ����ġ�� ����)�� ���� ���̵��
    // �� ���� ���� ������ ȣ���ϱ� ���� ���� Type�� mixed ��� ���� �߰��ϰ�, mixed�� ��� second stat ���� ���� ǥ��

    #region Reinforce Function
    public void ReinforceStat(int i) // i : 0~2 left, middle, right
    {
        // 0. �� �Լ��� 'Ư�� ����'�� �ƴ� ��쿡 �����.
        // 1. ���� ValueType �˻� (Percent / Fixed) -> �̱���
        // 2. ���� TargetStat �˻�
        // 3. ���������� Value�� ��� ���� -> �̱���

        if (ReinforceManager.valueType[i] == "Fixed")
        {
            // ���� : ����� ���� = ���� ���� + ��ȭ��
            // ���� : ���� ���� 100, ��ȭ�� 50
            // ��� : 100 + 50 = 150
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
            // ���� => ����� ���� = ���� ���� + ���� ����*0.01*��ȭ��
            // ���� : �������� 100, ��ȭ�� 5%
            // ��� : 100 + 100 * 0.01 * 5 = 100 + 5 = 105
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
        // Ư�� ���� ���� ��, if������ ���� ���� Ÿ�� �˻�
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
