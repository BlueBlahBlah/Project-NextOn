using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Semicolon : Enemy
{
    //[SerializeField] private bool isWalking;
    //[SerializeField] private bool isAttacking;
    private Animator _animator;
    private int health;         //현재 스크립트에 관리하는 체력 - Enemy와 비교홰 닳았는지 판단
    private bool IsDeath;
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth = GetComponent<Enemy>().maxHealth;
        this.curHealth = this.maxHealth;
        health = maxHealth;
        //isWalking = true;
        target = GameObject.Find("Player").transform;
        GetComponent<Enemy>().target = target;
        _animator = GetComponent<Animator>();
        _animator.SetBool("isWalking",true);
        IsDeath = false;
        damaged.SetText("");  //데미지를 입은 경우에만 표시
        InitHPBarSize();  //체력바 사이즈 초기화
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
                    int DamageDone = health - curHealth;       //입은 데미지.
                    ShowDamage(DamageDone);
                    hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
                    _animator.SetTrigger("Death");
                    GetComponent<Enemy>().isChase = false;
                    IsDeath = true;
                }
                else
                {
                    int DamageDone = health - curHealth;        //입은 데미지.
                    ShowDamage(DamageDone);
                    hpBar.rectTransform.localScale = new Vector3((float)curHealth/(float)maxHealth, 1f, 1f);
                    health = curHealth;
                    _animator.SetTrigger("Hit");
                }
            }
       
            //움직임 및 공격 관련
            //가까우면 공격
            if (Vector3.Distance(target.position, this.transform.position) < 3)
            {
                //isWalking = false;
                //isAttacking = true;
                _animator.SetBool("isWalking",false);
                _animator.SetBool("isAttacking",true);
                GetComponent<Enemy>().isChase = false;
                transform.LookAt(target);
            }
            else  //멀면 다시 쫒아가기
            {
                //isWalking = true;
                //isAttacking = false;
                _animator.SetBool("isWalking",true);
                _animator.SetBool("isAttacking",false);
                GetComponent<Enemy>().isChase = true;
            }
        }
    }

    private void ShowDamage(int d)
    {
        //CancelInvoke();         //기존의 데미지가 있었다면 해당 데미지 삭제 1초 타이머 종료 => 새로운 데미지를 받으면 그 데미지 1초동안 표시
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0,3.5f,0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
        //Invoke("HideDamage",1);
        
    }

    void InitHPBarSize()
    {
        //hpBar의 사이즈를 원래 자신의 사이즈의 1배 크기로 초기화
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}
