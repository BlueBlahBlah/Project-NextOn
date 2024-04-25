using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdgeSphere : MonoBehaviour
{
    private SphereCollider SphereCollider;
    public int Damage;

    private float TickTime;       //데미지를 주는 틱 간격
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
        TickTime = 0;
        Damage = 1;    //기본 스킬 데미지
    }

    // Update is called once per frame
    void Update()
    {
        TickTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && TickTime >= 0.25f)
        {
            //스킬계수 추가
            int TempDamage =  GameObject.Find("StageManager").GetComponent<StageManager>().SwordStreamEdge_Skill_DamageCounting * Damage;         
            other.GetComponent<Enemy>().curHealth -= TempDamage;
            TickTime = 0;
        }
    }
}
