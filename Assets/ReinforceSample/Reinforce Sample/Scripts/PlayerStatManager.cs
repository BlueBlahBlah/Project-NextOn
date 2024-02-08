using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
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

    [Header("Physical")]
    [SerializeField]
    public  float health;
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
}
