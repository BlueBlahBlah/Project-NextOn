using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkillTriggerBox : MonoBehaviour
{
    public GameObject plane;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Collision");
        if (other.CompareTag("Player"))
        {
            Debug.LogError("Player Detected");
            BomberSkill bomberSkill = plane.GetComponent<BomberSkill>();

            if (bomberSkill != null)
            {
                bomberSkill.Bomb();
            }
        }
    }
}
