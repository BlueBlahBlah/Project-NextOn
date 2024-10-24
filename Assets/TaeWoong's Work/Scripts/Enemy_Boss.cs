using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_Boss : Mob
{
    public bool isAttack; // 공격 여부
    public bool isDie; // 사망 여부
    public bool isLook; // 플레이어 바라보기 여부
    Vector3 lookVec; // 플레이어 예측 방향
    [SerializeField] private BoxCollider MeleeAttackArea; // 근접 공격 범위 콜라이더
    [SerializeField] private BoxCollider MagicAttackArea; // 원거리 공격 범위 콜라이더
    public int MeleeDamage = 5; // 몬스터의 근접 공격력
    public float damageInterval = 1.0f; // 데미지를 주는 간격 (초)
    public Transform magicTarget;
    public Boss_Meteor boss_Meteor; // 매테오 공격 오브젝트 참조
    public Boss_Boom boss_Boom; // 에너지 공격 오브젝트 참조
    public MobSpawner leftSummon; // MonsterSpawner를 참조
    public MobSpawner rightSummon; // MonsterSpawner를 참조
    [SerializeField] private TextMeshPro damaged; // 데미지 표시용 텍스트
    public Image hpBar; // 체력바 UI
    private int health; // 현재 스크립트에서 관리하는 체력
    private Rigidbody rigid;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        isLook = true;

        this.maxHealth = GetComponent<Mob>().maxHealth;
        this.curHealth = this.maxHealth;
        health = maxHealth;

        anim.SetBool("isBattle", true); // 전투 태세 애니메이션 활성화

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
        if (isDie)  // 죽은 경우
        {
           Die();
        }
        else    // 살아있는 경우
        {
            if (isLook)
            {
                float h = target.position.x - transform.position.x; // 수평 좌표
                float v = target.position.z - transform.position.z; // 수직 좌표

                lookVec = new Vector3(h, 0, v).normalized * 5f; // 방향 벡터 정규화
                transform.LookAt(target.position + lookVec);

                // 플레이어의 위치로부터 거리 계산
                float meleeDistance = Vector3.Distance(transform.position, MeleeAttackArea.bounds.center);
                float magicDistance = Vector3.Distance(transform.position, MagicAttackArea.bounds.center);

                bool isInMeleeRange = meleeDistance < MeleeAttackArea.bounds.extents.magnitude;
                bool isInMagicRange = magicDistance < MagicAttackArea.bounds.extents.magnitude;

                if (isInMeleeRange)
                {
                    // 근접 공격
                    if (!isAttack) // 공격 중이 아닐 때만 공격
                    {
                        StartCoroutine(MeleeAttack());
                        StartCoroutine(RandomRangeAttack());
                    }
                }
                else if (isInMagicRange)
                {
                    // 원거리 공격
                    if (!isAttack) // 공격 중이 아닐 때만 공격
                    {
                        StartCoroutine(RandomRangeAttack());
                    }
                }
                else
                {
                    // 플레이어가 범위 밖에 있을 경우 공격하지 않음
                    isAttack = false; // 공격 상태 초기화
                }
            }

            UpdateHPBar();

            this.curHealth = GetComponent<Mob>().curHealth;
            if (curHealth < health)
            {
                if (curHealth <= 0)
                {
                    int DamageDone = health - curHealth;       // 입은 데미지.
                    ShowDamage(DamageDone);
                    hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
                    Die();
                }
                else
                {
                    int DamageDone = health - curHealth;        // 입은 데미지.
                    ShowDamage(DamageDone);
                    hpBar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1f, 1f);
                    health = curHealth;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 시
        {
            if (!isDie && !isAttack)
            {
                isAttack = true; // 공격 상태 개시
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 AttackArea를 나갔을 때
        {
            if (isAttack)
            {
                isAttack = false; // 공격 상태 초기화
            }
        }
    }

    // void Attack()
    // {
    //     if (!isAttack) // 이미 공격 중이 아닌지 확인
    //     {
    //         isAttack = true; // 공격 상태로 변경
    //         StartCoroutine(Pattern()); // 공격이 끝난 후 상태를 처리하는 코루틴 호출
    //     }
    // }

    // IEnumerator Pattern()
    // {
    //     yield return new WaitForSeconds(1.0f);

    //     int ranAction = Random.Range(0, 4);

    //     switch (ranAction) {
    //         case 0: // 근거리 공격
    //             StartCoroutine(MeleeAtk());
    //             break;
    //         case 1: // 원거리 마법 (매테오)
    //             StartCoroutine(CastMeteor());
    //             break;
    //         case 2: // 원거리 마법 (몬스터 소환)
    //             StartCoroutine(SummonMob());
    //             break;
    //         case 3: // 원거리 마법 (에너지 폭발)
    //             StartCoroutine(EnergyBoom());
    //             break;
    //     }
    // }
    
    IEnumerator MeleeAttack()
    {
        isAttack = true; // 공격 시작
        anim.SetTrigger("isAttack01"); // 근접 공격 애니메이션
        Collider[] hitColliders = Physics.OverlapBox(MeleeAttackArea.bounds.center, MeleeAttackArea.bounds.extents, MeleeAttackArea.transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Player"))
            {
                PlayerManager.Instance.Health -= MeleeDamage; // 플레이어 데미지 처리
                yield return new WaitForSeconds(damageInterval); // 지정한 간격만큼 대기
            }
        }

        yield return new WaitForSeconds(1.0f);
        isAttack = false; // 공격 종료
    }
    private IEnumerator RandomRangeAttack()
    {
        // 랜덤하게 원거리 공격 선택
        int attackChoice = Random.Range(0, 3); // 랜덤으로 선택

        if (attackChoice == 0)
        {
            yield return StartCoroutine(CastMeteor());
        }
        else if (attackChoice == 1)
        {
            yield return StartCoroutine(EnergyBoom());
        }
        else
        {
            yield return StartCoroutine(SummonMob());
        }

        yield return new WaitForSeconds(2.0f); // 각 공격 사이에 딜레이 추가
    }

    IEnumerator CastMeteor()
    {
        anim.SetTrigger("isAttack02"); // 마법 공격 애니메이션
        yield return new WaitForSeconds(2.0f);

        ActivateMeteor(); // 예상 위치에 Boss_Meteor 활성화
    }

    IEnumerator SummonMob()
    {
        anim.SetTrigger("isAttack03"); // 마법 공격 애니메이션
        MapSoundManager.Instance.Summon_Mob_Sound();

        yield return new WaitForSeconds(3.0f);

        leftSummon.SpawnMonster(); // 몬스터 스폰
        rightSummon.SpawnMonster(); // 몬스터 스폰
    }

    IEnumerator EnergyBoom()
    {
        anim.SetTrigger("isAttack02"); // 마법 공격 애니메이션
        yield return new WaitForSeconds(2.0f);

        ActivateBoom(); // 예상 위치에 Boss_Boom 활성화
    }

    private void ActivateMeteor()
    {
        Vector3 spawnPosition = target.position; //+ lookVec; // 플레이어의 예상 이동 방향에 위치 설정
        Boss_Meteor meteorInstance = Instantiate(boss_Meteor, spawnPosition, Quaternion.identity);
        meteorInstance.gameObject.SetActive(true); // Boss_Meteor 활성화
    }

    private void ActivateBoom()
    {
        Vector3 spawnPosition = target.position;//  + lookVec; // 플레이어의 예상 이동 방향에 위치 설정
        Boss_Boom boomInstance = Instantiate(boss_Boom, spawnPosition, Quaternion.identity);
        boomInstance.gameObject.SetActive(true); // Boss_Boom 활성화
    }

    void UpdateHPBar()
    {
        hpBar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1f, 1f);
    }

    void Die()
    {
        if (isDie) return;
        
        anim.SetTrigger("doDie"); // 죽는 애니메이션 트리거
        MapSoundManager.Instance.Die_Boss_Sound();

        leftSummon.RemoveAllMonsters(); // 몬스터 디스폰
        rightSummon.RemoveAllMonsters(); // 몬스터 디스폰

        isDie = true; // 사망 상태로 변경
        isAttack = false; // 공격 상태 초기화
        isLook = false; // 플레이어 추적 중단

        rigid.isKinematic = true; // 물리적 상호작용 중지

        // if (GetComponent<Mob>().isChase == true) // update 안에서 오류가 너무 많이 발생하지 않도록 하기 위함
        // {
            // GetComponent<Mob>().stopNav();
            // GetComponent<Mob>().Death_Collider_False(); // 콜라이더 비활성화
        // }
        // GetComponent<Mob>().isChase = false;
    }
}
