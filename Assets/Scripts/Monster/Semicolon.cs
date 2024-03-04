using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semicolon : Enemy
{
    //[SerializeField] private bool isWalking;
    //[SerializeField] private bool isAttacking;
    private Animator _animator;
    private int health;         //현재 스크립트에 관리하는 체력 - Enemy와 비교홰 닳았는지 판단
    private bool IsDeath;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth = GetComponent<Enemy>().maxHealth;
        this.curHealth = this.maxHealth;
        health = maxHealth;
        target = GameObject.Find("Player").transform;
        _animator = GetComponent<Animator>();
        _animator.SetBool("isRunning",true);
        IsDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath == true)  //죽은경우
        {
            GetComponent<Enemy>().isChase = false;
            GetComponent<Enemy>().stopNav();
            Destroy(gameObject,3f);
        }
        else        //살아있는경우
        {
            //체력관련
            this.curHealth = GetComponent<Enemy>().curHealth;
            if (curHealth < health)
            {
                if (curHealth <= 0)
                {
                    _animator.SetTrigger("Death");
                    GetComponent<Enemy>().isChase = false;
                    IsDeath = true;
                }
                else
                {
                    health = curHealth;
                    _animator.SetTrigger("Hit");
                }
            }
       
            //움직임 및 공격 관련
            //가까우면 공격
            if (Vector3.Distance(target.position, this.transform.position) < 3)
            {
                _animator.SetBool("isRunning",false);
                _animator.SetBool("isAttacking",true);
                GetComponent<Enemy>().isChase = false;
                transform.LookAt(target);
            }
            else  //멀면 다시 쫒아가기
            {
                _animator.SetBool("isRunning",true);
                _animator.SetBool("isAttacking",false);
                GetComponent<Enemy>().isChase = true;
            }
        }
    }
}
