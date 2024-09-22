using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerScriptOneHand : MonoBehaviour
{
    public float moveSpeed = 0f;
    public bool walking;
    private Vector3 lastPosition;
    Animator Anim;
    [SerializeField] private int Damage;

    public Button attackBtn;

    [SerializeField] private BoxCollider DamageZone;
    //public Button RollBtn;

    [SerializeField] private SwordStreamOfEdge SwordStreamOfEdge;
    [SerializeField] private SwordStatic SwordStatic;
    [SerializeField] private SwordSilver SwordSilver;
    [SerializeField] private SwordDemacia SwordDemacia;
    [SerializeField] private FantasyAxe FantasyAxe;
    
    private bool temporary_death;           //임시 죽음 스위치 - Update 제어
    private bool stop_update;               //update 제어

    //[SerializeField] private DamageManager DamageManager;

    void Start()
    {
        // 초기 위치 저장
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        attackBtn.onClick.AddListener(OnAttackButtonClick);
        //RollBtn.onClick.AddListener(OnRollButtonClick);         //구르기버튼

        try
        {
            WeaponSynchronization();
        }
        catch (NullReferenceException e)
        {
            
        }
        temporary_death = false;
        stop_update = false;
    }

    public void WeaponSynchronization()
    {
        /*SwordStreamOfEdge = GameObject.Find("SwordStreamOfEgde").GetComponent<SwordStreamOfEdge>();
        SwordStatic = GameObject.Find("SwordStatic").GetComponent<SwordStatic>();
        SwordSilver = GameObject.Find("SwordSilver").GetComponent<SwordSilver>();
        SwordDemacia = GameObject.Find("SwordDemacia").GetComponent<SwordDemacia>();
        FantasyAxe = GameObject.Find("FantasyAxe_Unity").GetComponent<FantasyAxe>();*/
    }

    void Update()
    {
        if (stop_update == false)
        {
            if (PlayerManager.Instance.Health <= 0)        //체력이 다 닳은 경우
            {
                GetComponentInParent<CharacterLocomotion>().enabled = false;
                Anim.applyRootMotion = true;
                Anim.SetTrigger("Death");
                stop_update = true;
            }
            else
            {
            
                // 현재 위치와 이전 위치 비교
                if (transform.position != lastPosition)
                {
                    walking = true;
                    // 위치가 변경되었을 때만 아래 코드 실행

                    // 이동 방향 설정
                    Vector3 moveDirection = (transform.position - lastPosition).normalized;

                    // 움직임 처리
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                    // 회전 처리
                    Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);

                    // 현재 위치를 이전 위치로 업데이트
                    lastPosition = transform.position;
                }
                else
                {
                    walking = false;
                }
                Anim.SetBool("walk", walking);
            }
        }
        
        
    }

    public void OnAttackButtonClick()
    {
        Anim.SetTrigger("attack");
        //ColliderAttack();         //애니메이션에 이벤트로 넣어둠
    }
    
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }

    void ColliderAttack()       //애니메이션 호출
    {
        Damage = 0;
        if (SwordStreamOfEdge != null && SwordStreamOfEdge.gameObject.activeSelf)
        {
            Damage = SwordStreamOfEdge.GetComponent<SwordStreamOfEdge>().Damage;
            Damage *= DamageManager.Instance.SwordStreamEdge_DamageCounting;
        }
        else if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
        {
            Damage = SwordStatic.GetComponent<SwordStatic>().Damage;
            Damage *= DamageManager.Instance.SwordStatic_DamageCounting;
        }
        else if (SwordSilver != null && SwordSilver.gameObject.activeSelf)
        {
            Damage = SwordSilver.GetComponent<SwordSilver>().Damage;
            Damage *= DamageManager.Instance.SwordSliver_DamageCounting;
        }
        else if (SwordDemacia != null && SwordDemacia.gameObject.activeSelf)
        {
            Damage = SwordDemacia.GetComponent<SwordDemacia>().Damage;
            Damage *= DamageManager.Instance.SwordDemacia_DamageCounting;
        }
        else if (FantasyAxe != null && FantasyAxe.gameObject.activeSelf)
        {
            Damage = FantasyAxe.GetComponent<FantasyAxe>().Damage;
            Damage *= DamageManager.Instance.FantasyAxe_DamageCounting;
        }
        
        Collider[] hitColliders = Physics.OverlapBox(DamageZone.bounds.center, DamageZone.bounds.extents,
            DamageZone.transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                // 데미지 = 계수 * 무기 데미지
                col.GetComponent<Enemy>().curHealth -= Damage;         //계수 추가
                if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
                {
                    SwordStatic.GetComponent<SwordStatic>().attackNum++;        //스태틱의 경우 스택 추가
                }
            }
        }
    }
    
    private void after_Death_Animation()
    {
        //아직 부활이 남아있다면
        if (PlayerManager.Instance.revive >= 1)
        {
            temporary_death = true;
            Anim.SetTrigger("Revive");        //애니메이션 트리거
        }
    }
    
    private void start_Landing_Animation()
    {
        //몬스터 밀어내는 코드
        
        float pushRadius = 10f;  // 반경 10
        float pushForce = 5f;    // 밀어내는 힘
        float pushDuration = 1f; // 밀어내는 시간 (초)
        
        
        // 특정 반경 내의 모든 콜라이더를 찾음
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);

        foreach (Collider collider in hitColliders)
        {
            // Enemy 태그를 가진 오브젝트인지 확인
            if (collider.CompareTag("Enemy"))
            {
                // 오브젝트의 NavMeshAgent 가져옴
                NavMeshAgent agent = collider.GetComponent<NavMeshAgent>();

                if (agent != null)
                {
                    // 방향 계산: 플레이어에서 적까지의 벡터
                    Vector3 pushDirection = collider.transform.position - transform.position;
                    pushDirection.y = 0; // y축으로는 밀지 않으려면 y를 0으로 유지

                    // NavMeshAgent의 이동 속도에 힘 적용 (방향과 속도 설정)
                    Vector3 pushVelocity = pushDirection.normalized * pushForce;

                    // 적을 일정 시간 동안 밀어내는 코루틴 실행
                    StartCoroutine(PushEnemy(agent, pushVelocity));
                }
            }
        }
    }
    
    // 적을 일정 시간 동안 밀어내는 코루틴
    System.Collections.IEnumerator PushEnemy(NavMeshAgent agent, Vector3 pushVelocity)
    {
        float pushDuration = 1f; // 밀어내는 시간 (초)
        float elapsedTime = 0;

        // 밀어내는 동안 NavMeshAgent가 경로를 따라 움직이지 않도록 비활성화
        agent.isStopped = true;

        // pushDuration 동안 밀어내기
        while (elapsedTime < pushDuration)
        {
            agent.Move(pushVelocity * Time.deltaTime); // NavMeshAgent의 Move()로 직접 이동 적용
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 밀어낸 후 NavMeshAgent 재활성화
        agent.isStopped = false;
    }
    
    private void after_Landing_Animation()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        GetComponentInParent<CharacterLocomotion>().enabled = true;
        Anim.applyRootMotion = false;
        temporary_death = false;
        stop_update = false;
        //PlayerManager.Instance.Health = 100;
    }

}
