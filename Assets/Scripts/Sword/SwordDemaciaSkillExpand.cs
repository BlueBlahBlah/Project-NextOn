using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDemaciaSkillExpand : MonoBehaviour
{
    [SerializeField] private GameObject Sword1;
    [SerializeField] private GameObject[] Sword2;
    [SerializeField] private GameObject[] Sword3;
    // Start is called before the first frame update
    void Start()
    {
        Sword1.SetActive(true);
        foreach (var s in Sword2)
        {
            s.SetActive(false);
        }
        foreach (var s in Sword3)
        {
            s.SetActive(false);
        }

        Invoke("skill2", 1f);
        Invoke("skill3", 2f);
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
    
    void skill3()
    {
        foreach (var s in Sword3)
        {
            s.SetActive(true);
        }
    }
}
