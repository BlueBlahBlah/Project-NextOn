using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RamMonster : Enemy
{
    private Animator _animator;
    private int health;         //현재 스크립트에 관리하는 체력 - Enemy와 비교홰 닳았는지 판단
    private bool IsDeath;
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;
    [SerializeField] private Collider capsulCollider;
    [SerializeField] private BoxCollider AttackArea;
    [SerializeField] private Enemy _enemy;
    
    void Start()
    {
        this.maxHealth = GetComponent<Enemy>().maxHealth;
        this.curHealth = this.maxHealth;
        health = maxHealth;
        target = GameObject.Find("Player").transform;
        _enemy.target = target;
        _animator = GetComponent<Animator>();
        _animator.SetBool("isWalking",true);
        IsDeath = false;
        damaged.SetText(""); 
        capsulCollider = GetComponent<CapsuleCollider>();
        InitHPBarSize();  //체력바 사이즈 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath == true)  //죽은경우
        {
            _animator.SetTrigger("Death");
            if (GetComponent<Enemy>().isChase == true)                  //update안에서 오류가 너무 많이 발생하지 않도록하기 위함
            {
                GetComponent<Enemy>().stopNav();
                GetComponent<Enemy>().Death_Collider_False();           //콜라이더 비활성화
            }
            GetComponent<Enemy>().isChase = false;
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
                    capsulCollider.enabled = false;
                    _enemy.isChase = false;
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
       
            if (Vector3.Distance(target.position, this.transform.position) < 3)
            {
                _animator.SetBool("isWalking",false);
                _animator.SetBool("isAttacking",true);
                
                _enemy.isChase = false;
                transform.LookAt(target);
            }
            else 
            {
                _animator.SetBool("isWalking",true);
                _animator.SetBool("isAttacking",false);
                _enemy.isChase = true;
            }
        }
    }

    public void MonsterClear()
    {
        this.IsDeath = true;
    }

    private void ShowDamage(int d)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0,3.5f,0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
    }

    void InitHPBarSize()
    {
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    
    void ColliderAttack()
    {
        int Damage = 10;
        
        Collider[] hitColliders = Physics.OverlapBox(AttackArea.bounds.center, AttackArea.bounds.extents,
            AttackArea.transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Player"))
            {
                PlayerManager.Instance.Health -= Damage;
            }
        }
    }
    
}
