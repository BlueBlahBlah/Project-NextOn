using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasyAxwSkill : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject[] shelds;
    [SerializeField] private GameObject[] magicEffect;
    //[SerializeField] private DamageManager DamageManager;

    [SerializeField] private float DamageZone;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        explosionEffect.SetActive(false);
        Invoke("explore",10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void explore()
    {
        int TempDamage = DamageManager.Instance.FantasyAxe_Skill_DamageCounting * Damage;
        explosionEffect.SetActive(true);
        foreach (GameObject VARIABLE in shelds)
        {
            VARIABLE.SetActive(false);
        }
        foreach (GameObject VARIABLE in magicEffect)
        {
            VARIABLE.SetActive(false);
        }

        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, DamageZone);
        if (colls.Length != 0)
        {
            foreach (Collider target in colls)
            {
                if (target.CompareTag("Enemy"))
                {
                    //공격 로직
                    target.GetComponent<Enemy>().curHealth -= TempDamage;
                }
            }
        }
        
        Destroy(gameObject,1f);
    }
}
