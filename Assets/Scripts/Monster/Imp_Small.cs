using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp_Small : Enemy
{
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isAttacking;
    private Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth = 10;
        this.curHealth = this.maxHealth;
        isWalking = true;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking",true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, this.transform.position) < 3)
        {
            isWalking = false;
            isAttacking = true;
            anim.SetBool("isWalking",false);
            anim.SetBool("isAttacking",true);
            GetComponent<Enemy>().isChase = false;
        }
        else
        {
            isWalking = true;
            isAttacking = false;
            anim.SetBool("isWalking",true);
            anim.SetBool("isAttacking",false);
            GetComponent<Enemy>().isChase = true;
        }
    }
}
