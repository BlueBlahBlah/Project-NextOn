using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDemaciaSkill : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject effect;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private bool attackDone;

    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        sword.SetActive(true);
        effect.SetActive(false);
        attackDone = false;
        Damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.3F && attackDone == false)    //일정수준 스킬이 내려온경우
        {
            attackDone = true;
            skillStart();
            Destroy(gameObject, 3f);
        }
        
    }

    void skillStart()
    {
        rigid.velocity = Vector3.zero;                                  //움직임 그만
        rigid.angularVelocity = new Vector3(0, 0, 0);             //회전 그만
        effect.SetActive(true);
        SkillAttack();
       
    }
    

    void SkillAttack()
    {
        int TempDamage =  GameObject.Find("StageManager").GetComponent<StageManager>().SwordDemacia_Skill_DamageCounting * Damage;
        //전달받은 몬스터를 중심으로 맞닿은 물체 탐색
        Collider[] colliders = Physics.OverlapBox(
            transform.position,
            GetComponent<Collider>().bounds.extents,
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
