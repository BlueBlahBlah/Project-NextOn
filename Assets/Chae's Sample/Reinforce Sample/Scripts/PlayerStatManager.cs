using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // PlayerStatManager 의 경우 증강과 다르게, 인게임 시작 전에서도 캐릭터 변경 등의 이유로
    // Stat 변경이 존재할 수 있기 때문에 다른 씬에서도 고유한 인스턴스만 존재하도록 싱글톤으로 작성하였음

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

    // 구르기 삭제에 따라 stamina 관련 요소는 사용처가 불분명해져 보류
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

    // absorption (생명력 흡수) 의 경우 게임이 너무 쉬워지거나 하는 밸런스에 영향을 줄 가능성이 높아 보류 
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
