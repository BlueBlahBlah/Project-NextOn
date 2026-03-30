using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdgeSphere : MonoBehaviour
{
    private SphereCollider SphereCollider;
    public int Damage;
    //[SerializeField] private DamageManager DamageManager;
    private float TickTime;       //?곕?吏瑜?二쇰뒗 ??媛꾧꺽
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
        TickTime = 0;
        Damage = 1;    //湲곕낯 ?ㅽ궗 ?곕?吏
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
            //?ㅽ궗怨꾩닔 異붽?
            int TempDamage = DamageManager.Instance.SwordStreamEdge_Skill_DamageCounting * Damage;         
            other.GetComponent<Enemy>().CurHealth -= TempDamage;
            TickTime = 0;
        }
    }
}
