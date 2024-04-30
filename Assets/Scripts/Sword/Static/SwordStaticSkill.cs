using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStaticSkill : MonoBehaviour
{
    [SerializeField] private BoxCollider Collider;
    public int Damage;
    [SerializeField] private DamageManager DamageManager;
    void Start()
    {
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
        Damage = 10;
        Invoke("Attack",1f);
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void Attack()
    {
        int TempDamage = DamageManager.SwordStatic_Skill_DamageCounting * Damage;
        //전달받은 몬스터를 중심으로 맞닿은 물체 탐색
        Collider[] colliders = Physics.OverlapBox(
            Collider.transform.position,
            Collider.size / 2,
            Quaternion.identity
        );
        //맞닿은 물체중에 해당 스킬 오브젝트가 있다면 true
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy")) 
            {
                collider.GetComponent<Enemy>().curHealth -= TempDamage;
            }
        }
    }
   
}
