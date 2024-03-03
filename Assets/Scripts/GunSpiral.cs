using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunspiral : MonoBehaviour
{
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        Damage = 10;
        attack();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack()
    {
        int TempDamage =  GameObject.Find("StageManager").GetComponent<StageManager>().GunSpire_Skill_DamageCounting * Damage;
        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);
        if (colls.Length == 0)
        {
            Destroy(gameObject,5);
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().curHealth -= TempDamage ;
            }
        }
    }
}
