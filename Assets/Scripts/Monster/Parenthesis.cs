using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenthesis : Enemy
{

    public enum Identity
    {
        Big,
        Medium,
        Small
    }
    public Identity identity;
    
    private Animator _animator;
    private int health;         //현재 스크립트에 관리하는 체력 - Enemy와 비교홰 닳았는지 판단
    private bool isDeath;
    [SerializeField]private bool isInStack;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth = GetComponent<Enemy>().maxHealth;
        this.curHealth = this.maxHealth;
        health = maxHealth;
        target = GameObject.Find("Player").transform;
        _animator = GetComponent<Animator>();
        _animator.SetBool("isRunning",true);
        isDeath = false;
        isInStack = false;
        this.GetComponent<Enemy>().target = GameObject.Find("Player").transform;
        this.target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath == true)  //죽은경우
        {
            GetComponent<Enemy>().isChase = false;
            GetComponent<Enemy>().stopNav();
        }
        else        //살아있는경우
        {
            //체력관련
            this.curHealth = GetComponent<Enemy>().curHealth;
            if (curHealth < health)
            {
                if (curHealth <= 0 && isInStack == false)
                {
                    isInStack = true;
                    health = curHealth;
                    //체력이 다 닳은 경우 아직 죽지말고 스택에 추가
                    GameObject.Find("StageManager").GetComponent<StageManager>().AddStackMonster(this.gameObject);
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

    //괄호가 채워지는 경우 호출될 함수
    public void HitTheMonster()
    {
        //스택에 들어가 있는 경우 처치
        if (isInStack == true)
        {
            ClearTheMonster();
        }
    }
    //몬스터를 처치할 때 호출되는 함수
    public void ClearTheMonster()
    {
        _animator.SetTrigger("Death");
        GetComponent<Enemy>().isChase = false;
        isDeath = true;
        //3초후 삭제
        Destroy(gameObject,3f);
    }
    //스택이 다 찬경우에 몬스터가 처치될 시 호출
    //괄호 몬스터가 죽지 않는 코드
    public void NotDeath()
    {
        this.curHealth = maxHealth;         //체력 만땅
        this.isInStack = false;             //스택에 들어가지 않았음
    }
}
