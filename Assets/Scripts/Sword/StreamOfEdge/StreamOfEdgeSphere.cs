using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdgeSphere : MonoBehaviour
{
    private SphereCollider SphereCollider;
    public int Damage;
    //[SerializeField] private DamageManager DamageManager;
    private float TickTime;       //?°ë?ì§€ë¥?ì£¼ëŠ” ??ê°„ê²©
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
        TickTime = 0;
        Damage = 1;    //ê¸°ë³¸ ?¤í‚¬ ?°ë?ì§€
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
            //?¤í‚¬ê³„ìˆ˜ ì¶”ê?
            int TempDamage = DamageManager.Instance.SwordStreamEdge_Skill_DamageCounting * Damage;         
            other.GetComponent<Enemy>().CurHealth -= TempDamage;
            TickTime = 0;
        }
    }
}
