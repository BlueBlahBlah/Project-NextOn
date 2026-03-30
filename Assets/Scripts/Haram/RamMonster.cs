using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RamMonster : Enemy
{
    private Animator _animator;
    private int health;         
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
        InitHPBarSize();
    }

    void Update()
    {
        if (IsDeath == true)
        {
            _animator.SetTrigger("Death");
            if (GetComponent<Enemy>().isChase == true)
            {
                GetComponent<Enemy>().stopNav();
                GetComponent<Enemy>().Death_Collider_False();
            }
            GetComponent<Enemy>().isChase = false;
            Destroy(gameObject,3f);
        }
        else
        {
            this.curHealth = GetComponent<Enemy>().curHealth;
            if (curHealth < health)
            {
                if (curHealth <= 0)
                {
                    int DamageDone = health - curHealth;
                    ShowDamage(DamageDone);
                    hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
                    _animator.SetTrigger("Death");
                    capsulCollider.enabled = false;
                    _enemy.isChase = false;
                    IsDeath = true;
                }
                else
                {
                    int DamageDone = health - curHealth;
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
