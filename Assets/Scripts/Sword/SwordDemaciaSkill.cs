using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDemaciaSkill : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject effect;
    [SerializeField] private Rigidbody rigid;

    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        sword.SetActive(true);
        effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.2F)    //일정수준 스킬이 내려온경우
        {
            skillStart();
            Destroy(gameObject, 3f);
        }
        
    }

    void skillStart()
    {
        rigid.velocity = Vector3.zero;                                  //움직임 그만
        rigid.angularVelocity = new Vector3(0, 0, 0);             //회전 그만
        effect.SetActive(true);

        /*Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);
        if (colls.Length == 0)      //반경에 아무것도 없는 경우
        {
            Destroy(gameObject,1.5f);
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))       //Enemy tag를 가진경우
            {
                //공격하는 매커니즘
                Debug.Log("유탄 공격성공");
            }
        }*/
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        Debug.Log("데마시아 충돌");
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            Debug.Log("데마시아 스킬");
        }
    }
}
