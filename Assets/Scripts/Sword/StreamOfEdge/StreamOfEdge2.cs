using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdge2 : MonoBehaviour
{
    private Transform[] currentTarget;
    private float TickTime;
    public int Damage;

    void Start()
    {
        TickTime = 0;
        Destroy(gameObject, 10f);
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, 0f);
        Damage = 1;
    }

    void Update()
    {
        TickTime += Time.deltaTime;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && TickTime >= 0.25f)
        {
            int TempDamage = DamageManager.Instance.SwordStreamEdge_Skill_DamageCounting * Damage;   
            other.GetComponent<Enemy>().CurHealth -= TempDamage;
            TickTime = 0;
        }
    }
}
