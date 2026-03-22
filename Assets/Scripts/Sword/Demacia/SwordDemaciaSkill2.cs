using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDemaciaSkill2 : MonoBehaviour
{
    [SerializeField] private GameObject Sword1;
    [SerializeField] private GameObject[] Sword2;
    
    // Start is called before the first frame update
    void Start()
    {
        Sword1.SetActive(true);
        foreach (var s in Sword2)
        {
            s.SetActive(false);
        }
        
        Invoke("skill2", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void skill2()
    {
        foreach (var s in Sword2)
        {
            s.SetActive(true);
        }
    }
    
   
}
