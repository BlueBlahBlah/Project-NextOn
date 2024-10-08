using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_Turtle : Enemy
{
    public bool isAttack; // 공격 여부
    public bool isDie; // 사망 여부
    [SerializeField] private BoxCollider AttackArea; // 공격 범위 콜라이더
    public int Damage = 5; // 몬스터의 공격력
    public float damageInterval = 1.0f; // 데미지를 주는 간격 (초)
    [SerializeField] private TextMeshPro damaged; // 데미지 표시용 텍스트
    public Image hpBar; // 체력바 UI
    public MobSpawner mobSpawner; // MobSpawner 참조
    private int health; //현재 스크립트에서 관리하는 체력
    private bool canDamagePlayer = true; // 플레이어에게 데미지를 줄 수 있는 상태

    private Rigidbody _rigid;
    private NavMeshAgent _nav;
    [SerializeField] private Animator _anim;
    [SerializeField] private Collider _capsuleCollider;

    void Start() 
    {
        _rigid = GetComponent<Rigidbody>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        target = GameObject.Find("Player").transform;

        this.maxHealth = GetComponent<Enemy>().maxHealth;
        this.curHealth = this.maxHealth;
        health = maxHealth;

        damaged.SetText("");  // 데미지를 입은 경우에만 표시
        InitHPBarSize();  // 체력바 사이즈 초기화
    }
   
    void InitHPBarSize()
    {
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void ShowDamage(int d)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0, 3.5f, 0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
        UpdateHPBar(); // 체력바 업데이트
    }

    void Update()
    {
        UpdateHPBar();

        // 체력 관련 처리
        if (isDie)  //죽은경우
        {
           Die();
        }
        else    //살아있는경우
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
                    Die();
                }
                else
                {
                    int DamageDone = health - curHealth;        //입은 데미지.
                    ShowDamage(DamageDone);
                    hpBar.rectTransform.localScale = new Vector3((float)curHealth/(float)maxHealth, 1f, 1f);
                    health = curHealth;
                    
                    // 데미지 애니메이션이 끝났는지 체크
                    if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("isDamage"))
                    {
                        _anim.SetTrigger("isDamage");
                    }
                    else
                    {
                        // 데미지 애니메이션이 끝났다면 걷기 애니메이션으로 전환
                        _anim.SetBool("isWalk", true);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 시
        {
            Attack(); // 공격 메서드 호출
        }
    }

    void Attack()
    {
        if (!isAttack && canDamagePlayer) // 이미 공격 중이 아닌지 확인하고, 플레이어에게 데미지를 줄 수 있는지 확인
        {
            isAttack = true; // 공격 상태로 변경
            _anim.SetBool("isWalk", false); // 이동 애니메이션 중지
            _nav.isStopped = true; // NavMeshAgent 정지

            int attackType = Random.Range(0, 2); // 0 또는 1을 랜덤으로 선택
            if (attackType == 0)
            {
                _anim.SetTrigger("isAttack01"); // 첫 번째 공격 애니메이션
            }
            else
            {
                _anim.SetTrigger("isAttack02"); // 두 번째 공격 애니메이션
            }

            StartCoroutine(HandleAttack()); // 공격이 끝난 후 상태를 처리하는 코루틴 호출
        }
    }

    IEnumerator HandleAttack()
    {
        canDamagePlayer = false; // 플레이어에게 데미지를 줄 수 없는 상태로 변경

        Collider[] hitColliders = Physics.OverlapBox(AttackArea.bounds.center, AttackArea.bounds.extents, AttackArea.transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Player"))
            {
                PlayerManager.Instance.Health -= Damage; // 플레이어 데미지 처리
                yield return new WaitForSeconds(damageInterval); // 지정한 간격만큼 대기
            }
        }

        yield return new WaitForSeconds(1.0f); // 공격 애니메이션의 길이에 맞춰 대기

        isAttack = false; // 공격 상태 초기화
        _nav.isStopped = false; // NavMeshAgent 재개
        canDamagePlayer = true; // 플레이어에게 데미지를 줄 수 있는 상태로 변경
        
        // 공격 애니메이션이 끝난 후 걷기 애니메이션으로 전환
        _anim.SetBool("isWalk", true); // 걷기 애니메이션 활성화
        _anim.SetBool("isAttack01", false); // 공격 애니메이션 비활성화
        _anim.SetBool("isAttack02", false); // 공격 애니메이션 비활성화
    }

    void UpdateHPBar()
    {
        hpBar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1f, 1f);
    }

    void Die()
    {
        if (isDie) return; // 이미 죽은 경우 추가 처리 방지

        _anim.SetTrigger("doDie"); // 죽는 애니메이션 트리거
        isDie = true; // 사망 상태로 변경
        isAttack = false; // 공격 상태 초기화

        // Rigidbody의 물리적 힘 비활성화
        _rigid.isKinematic = true; // 물리적 상호작용 중지

        if (GetComponent<Enemy>().isChase == true) //update안에서 오류가 너무 많이 발생하지 않도록하기 위함
        {
            GetComponent<Enemy>().stopNav();
            GetComponent<Enemy>().Death_Collider_False(); //콜라이더 비활성화
        }
        GetComponent<Enemy>().isChase = false;

        // 몬스터가 죽었을 때 mobSpawner의 RemoveMonster 호출
        mobSpawner.RemoveMonster(gameObject); // 몬스터 제거

        // 애니메이션이 끝나기를 기다린 후 삭제
        StartCoroutine(WaitAndDestroy(4f)); // 4초 후 삭제
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // 애니메이션 시간 대기
        Destroy(gameObject); // 오브젝트 삭제
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 AttackArea를 나갔을 때
        {
            if (isAttack)
            {
                isAttack = false; // 공격 상태 초기화
                _nav.isStopped = false; // NavMeshAgent 재개
                _anim.SetBool("isWalk", true); // 걷는 애니메이션 활성화
            }
        }
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            _rigid.velocity = Vector3.zero; // 가속도 제어
            _rigid.angularVelocity = Vector3.zero; // 회전 제어
        }
    }
}
