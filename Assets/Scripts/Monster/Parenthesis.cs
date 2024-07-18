using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    public bool Waiting_Mate;
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;
    [SerializeField] private Collider capsulCollider;
    [SerializeField] private BoxCollider AttackArea;

    [SerializeField] private float MsgTimer;
    [SerializeField] private float MsgAgainTime;

    [SerializeField] public GameObject Mate_Monster;

    public void Set_Mate_Monster(GameObject m)
    {
        this.Mate_Monster = m;
    }
    
    
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
        Waiting_Mate = false;
        this.GetComponent<Enemy>().target = GameObject.Find("Player").transform;
        this.target = GameObject.Find("Player").transform;
        GetComponent<Enemy>().target = target;
        capsulCollider = GetComponent<CapsuleCollider>();
        InitHPBarSize();  //체력바 사이즈 초기화
        MsgTimer = 0f;
        MsgAgainTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        MsgTimer -= Time.deltaTime;
        if (isDeath == true)  //죽은경우
        {
            GetComponent<Enemy>().isChase = false;
            GetComponent<Enemy>().stopNav();
            _animator.SetTrigger("Death");
            capsulCollider.enabled = false;
            //3초후 삭제
            Destroy(gameObject,3f);
        }
        else        //살아있는경우
        {
            //체력관련
            this.curHealth = GetComponent<Enemy>().curHealth;
            if (curHealth < health)                             //체력이 감소한 경우
            {
                if (curHealth <= 0 && Waiting_Mate == false)    //처음 체력이 다 닳은 경우
                {
                    Waiting_Mate = true;
                    health = curHealth;
                    hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
                }
                else    
                {
                    if (Waiting_Mate == false)         //아직 체력이 남아있는 경우
                    {
                        int DamageDone = health - curHealth;        //입은 데미지
                        ShowDamage(DamageDone);
                        hpBar.rectTransform.localScale = new Vector3((float)curHealth/(float)maxHealth, 1f, 1f);
                    }
                    else if(Waiting_Mate == true && MsgTimer <= 0f)    //체력이 다 닳았으나 짝이 처치되지 않은 경우
                    {
                        if (Mate_Monster.GetComponent<Parenthesis>().Waiting_Mate == true)      //짝도 체력이 다 닳은 경우
                        {
                            isDeath = true;
                        }
                        else                //짝의 체력이 다 닳지 않은경우
                        {
                            ShowDamage("닫는 괄호 몬스터를 처치해야 합니다!");
                            MsgTimer = MsgAgainTime;
                        }
                        
                    }
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
    /*public void HitTheMonster()
    {
        //스택에 들어가 있는 경우 처치
        if (Waiting_Mate == true)
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
        capsulCollider.enabled = false;
        //3초후 삭제
        Destroy(gameObject,3f);
    }*/
    //스택이 다 찬경우에 몬스터가 처치될 시 호출
    //괄호 몬스터가 죽지 않는 코드
    /*public void NotDeath()
    {
        this.curHealth = maxHealth;         //체력 만땅
        this.Waiting_Mate = false;             //스택에 들어가지 않았음
    }*/
    
    private void ShowDamage(int d)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0,3.5f,0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
    }
    private void ShowDamage(string s)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0,3.5f,0), Quaternion.identity);
        tempDamage.SetText(s);
    }
    void InitHPBarSize()
    {
        //hpBar의 사이즈를 원래 자신의 사이즈의 1배 크기로 초기화
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    
    void ColliderAttack()       //애니메이션 호출
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
