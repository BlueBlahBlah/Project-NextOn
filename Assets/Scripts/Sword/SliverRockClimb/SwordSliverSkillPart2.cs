using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSliverSkillPart2 : MonoBehaviour
{
    
    [SerializeField] private SwordSilverSkill2 Parent;
    public int Damage;
    void Start()
    {
        Invoke("SecondAttack", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        int TempDamage = DamageManager.Instance.SwordSliver_Skill_DamageCounting * Damage;
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && IsAlready1Attack(enemy) == false)      
            {
                enemy.CurHealth -= TempDamage;
                Parent.enemyAgain.Add(enemy);
            }
        }
    }

    bool IsAlready1Attack(Enemy e) //?대? 1?瑜?留욎? 紐ъ뒪?곗씤吏 ?먮떒
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
        int TempDamage = DamageManager.Instance.SwordSliver_Skill_DamageCounting * Damage;
        foreach (var enemy in Parent.enemyAgain)
        {
            if (enemy != null && enemy.gameObject.activeInHierarchy)
            {
                // Check if the enemy is in contact with SwordSilverEffect
                if (ISContact(enemy.gameObject))
                {
                    enemy.CurHealth -= TempDamage;
                }
            }
        }
    }

    bool ISContact(GameObject enemyObject)
    {
        //?꾨떖諛쏆? 紐ъ뒪?곕? 以묒떖?쇰줈 留욌떯? 臾쇱껜 ?먯깋
        Collider[] colliders = Physics.OverlapBox(
            enemyObject.transform.position,
            enemyObject.GetComponent<Collider>().bounds.extents,
            Quaternion.identity
        );
        //留욌떯? 臾쇱껜以묒뿉 ?대떦 ?ㅽ궗 ?ㅻ툕?앺듃媛 ?덈떎硫?true
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
