using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasyAxeSkill : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject[] shelds;
    [SerializeField] private GameObject[] magicEffect;
    [SerializeField] private float time;
    [SerializeField] private float radius;
    
    void Start()
    {
        explosionEffect.SetActive(false);
        Invoke("explore",time);
    }

    void Update()
    {
        
    }

    void explore()
    {
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
        colls = Physics.OverlapSphere(transform.position, radius);
        if (colls.Length != 0)
        {
            foreach (Collider target in colls)
            {
                if (target.CompareTag("Enemy"))
                {
                    Debug.Log("Attack");
                }
            }
        }
        
        Destroy(gameObject,1f);
    }
}
