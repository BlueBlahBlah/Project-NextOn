using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : CreatureInfo
{
    public Transform target;
    public NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        nav.SetDestination(target.position);
    }

    public void Damage(int damage)
    {
        int currentHp = GetHP();
        if (currentHp > 0)
        {
            SetHP(currentHp - damage);
        }
        
        if (GetHP() <= 0)
        {
            Destroy(gameObject);
        }
    }
}
