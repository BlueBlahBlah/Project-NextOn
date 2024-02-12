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
    // Start is called before the first frame update
    void Start()
    {
        explosionEffect.SetActive(false);
        Invoke("explore",time);
    }

    // Update is called once per frame
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
                    //공격 로직
                    Debug.Log("마오카이 궁");
                }
            }
        }
        
        Destroy(gameObject,1f);
    }
}
