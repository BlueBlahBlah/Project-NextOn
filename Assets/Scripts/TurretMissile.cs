using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissile : MonoBehaviour
{
    [SerializeField] private DamageManager DamageManager;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
        Damage = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        int TempDamage = DamageManager.Turret_Skill_DamageCounting * Damage;
        if (other.CompareTag("Enemy"))
        {
            //적을 공격
            other.GetComponent<Enemy>().curHealth -= TempDamage ;
        }    
        
    }
}
