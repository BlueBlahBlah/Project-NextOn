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
