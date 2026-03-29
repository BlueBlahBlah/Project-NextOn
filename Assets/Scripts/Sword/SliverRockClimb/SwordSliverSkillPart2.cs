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

    bool IsAlready1Attack(Enemy e) //?´ë? 1?€ë¥?ë§ì? ëª¬ìŠ¤?°ì¸ì§€ ?ë‹¨
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
        //?„ë‹¬ë°›ì? ëª¬ìŠ¤?°ë? ì¤‘ì‹¬?¼ë¡œ ë§ë‹¿?€ ë¬¼ì²´ ?ìƒ‰
        Collider[] colliders = Physics.OverlapBox(
            enemyObject.transform.position,
            enemyObject.GetComponent<Collider>().bounds.extents,
            Quaternion.identity
        );
        //ë§ë‹¿?€ ë¬¼ì²´ì¤‘ì— ?´ë‹¹ ?¤í‚¬ ?¤ë¸Œ?íŠ¸ê°€ ?ˆë‹¤ë©?true
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
