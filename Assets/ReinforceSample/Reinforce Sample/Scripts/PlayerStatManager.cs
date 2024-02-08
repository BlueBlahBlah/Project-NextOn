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
}
