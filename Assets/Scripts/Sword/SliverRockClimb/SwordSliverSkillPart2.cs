using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSliverSkillPart2 : MonoBehaviour
{
    
    [SerializeField] private SwordSilverSkill2 Parent;
    [SerializeField] private DamageManager DamageManager;
    public int Damage;
    void Start()
    {
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
        Invoke("SecondAttack", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        int TempDamage = DamageManager.SwordSliver_Skill_DamageCounting * Damage;
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && IsAlready1Attack(enemy) == false)      
            {
                enemy.curHealth -= TempDamage;
                Parent.enemyAgain.Add(enemy);
            }
        }
    }

    bool IsAlready1Attack(Enemy e) //이미 1타를 맞은 몬스터인지 판단
    {
        foreach (Enemy v in Parent.enemyAgain)
        {
            if (v == e)
                return true;
        }

        return false;
    }

    void SecondAttack()
    {
        int TempDamage =  GetComponent<StageManager>().SwordSliver_Skill_DamageCounting * Damage;
        foreach (var enemy in Parent.enemyAgain)
        {
            if (enemy != null && enemy.gameObject.activeInHierarchy)
            {
                // Check if the enemy is in contact with SwordSilverEffect
                if (ISContact(enemy.gameObject))
                {
                    enemy.curHealth -= TempDamage;
                }
            }
        }
    }

    bool ISContact(GameObject enemyObject)
    {
        //전달받은 몬스터를 중심으로 맞닿은 물체 탐색
        Collider[] colliders = Physics.OverlapBox(
            enemyObject.transform.position,
            enemyObject.GetComponent<Collider>().bounds.extents,
            Quaternion.identity
        );
        //맞닿은 물체중에 해당 스킬 오브젝트가 있다면 true
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) 
            {
                return true;
            }
        }

        return false;
    }
}
