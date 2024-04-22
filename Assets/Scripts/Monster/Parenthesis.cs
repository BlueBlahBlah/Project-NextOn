using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    [SerializeField]private bool isInStack;
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;
    [SerializeField] private Collider capsulCollider;
    
    
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
        GetComponent<Enemy>().target = target;
        capsulCollider = GetComponent<CapsuleCollider>();
        InitHPBarSize();  //체력바 사이즈 초기화
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
                if (curHealth <= 0 && isInStack == false)    //체력이 다 닳고 스택에 들어가는 경우
                {
                    isInStack = true;
                    health = curHealth;
                    //체력이 다 닳은 경우 아직 죽지말고 스택에 추가
                    GameObject.Find("StageManager").GetComponent<StageManager>().AddStackMonster(this.gameObject);
                    hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
                }
                else    //체력이 다 닳아 스택에 들어가 있는 경우
                {
                    if (isInStack == false)         //아직 체력이 남아있고 스택에 들어가지 않은 경우
                    {
                        int DamageDone = health - curHealth;        //입은 데미지
                        ShowDamage(DamageDone);
                        hpBar.rectTransform.localScale = new Vector3((float)curHealth/(float)maxHealth, 1f, 1f);
                    }
                    else if(isInStack == true)    //스택에 있는 경우 닫는 괄호를 만들어야 한다는 메세지
                    {
                        ShowDamage("닫는 괄호 몬스터를 처치해야 합니다!");
                        
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
        capsulCollider.enabled = false;
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
}
