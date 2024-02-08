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
    private float healthRegen;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float staminaRegen;
    [SerializeField]
    private float depense;
    [SerializeField]
    private float speed;

    [Header("Attack")]
    [SerializeField]
    private float skillCooldown;
    [SerializeField]
    private float skillDamage;
    [SerializeField]
    private float critProbability;
    [SerializeField]
    private float critDamage;
    [SerializeField]
    private float absorption;
}
