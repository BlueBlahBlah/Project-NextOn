using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // PlayerStatManager �� ��� ������ �ٸ���, �ΰ��� ���� �������� ĳ���� ���� ���� ������
    // Stat ������ ������ �� �ֱ� ������ �ٸ� �������� ������ �ν��Ͻ��� �����ϵ��� �̱������� �ۼ��Ͽ���

    public static PlayerStatManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UpdateFinalDamage();
    }

    [Header("Physical")]
    [SerializeField]
    public float health;
    [SerializeField]
    public float healthRegen;
    [SerializeField]
    public float depense;
    [SerializeField]
    public float speed;

    // ������ ������ ���� stamina ���� ��Ҵ� ���ó�� �Һи����� ����
    // [SerializeField]
    // private float stamina;
    // [SerializeField]
    // private float staminaRegen;

    [Header("Attack")]
    [SerializeField]
    public float damage;
    [SerializeField]
    public float attackSpeed;
    [SerializeField]
    public float skillCooldown;
    [SerializeField]
    public float skillDamage;
    [SerializeField]
    public float critProbability;
    [SerializeField]
    public float critDamage;

    // absorption (����� ���) �� ��� ������ �ʹ� �������ų� �ϴ� �뷱���� ������ �� ���ɼ��� ���� ���� 
    // [SerializeField]
    // private float absorption;

    [Header("Final Damage")]
    [SerializeField]
    private float finalDamage;
    [SerializeField]
    private float finalCritDamage;
    [SerializeField]
    private float finalSkillDamage;
    [SerializeField]
    private float finalSkillCritDamage;

    public float FinalDamage => finalDamage;
    public float FinalCritDamage => finalCritDamage;
    public float FinalSkillDamage => finalSkillDamage;
    public float FinalSkillCritDamage => finalSkillCritDamage;


    public void UpdateFinalDamage()
    {
        finalDamage = DamageFormula.UpdateDamage();
        finalCritDamage = DamageFormula.UpdateCritDamage();
        finalSkillDamage = DamageFormula.UpdateSkillDamage();
        finalSkillCritDamage = DamageFormula.UpdateSkillCritDamage();
    }
    
}
