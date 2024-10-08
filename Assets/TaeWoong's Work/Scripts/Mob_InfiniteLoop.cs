using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; // Nav 관련 클래스는 UnityEngine.AI 네임스페이스 사용

public class MobInfiniteLoop : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target; // 추적 할 타겟
    public bool isChase; // 추적을 결정하는 bool 변수       
    Rigidbody rigid;
    BoxCollider boxCollider;
    // Material mat;
    NavMeshAgent nav; // Nav Agent를 사용하기 위해서는 Nav Mesh 생성 필수
    // NavMesh : NavAgent가 경로를 그리기 위한 바탕(Mesh)
    Animator anim;

    public BreakObject breakObject;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        // Enemy 오브젝트의 자식 컴포넌트에 MeshRender가 존재하기에 Children으로 부터 불러오기
        //mat = GetComponentInChildren<MeshRenderer>().material; // Material은 Mesh Render 컴포넌트를 통해서 접근 가능
        nav = GetComponent<NavMeshAgent>();
        // Enemy 오브젝트의 컴포넌트에 Animator가 존재하기에 불러오기
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator가 발견되지 않았습니다. Animator를 확인하세요.");
        }

        Invoke("ChaseStart", 1); // 1초 뒤 추적 개시
    }
   
   void ChaseStart() // 추적을 결정하는 bool 변수 사용을 제어하는 함수
   {
        isChase = true;
        anim.SetBool("isWalk", true);
   }

     // 외부 충돌에 의한 RigidBody의 물리적 문제 해결
     // 물리력이 NavAgent의 이동을 방해하지 않도록 로직 추가
   void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero; // 가속도 제어
            rigid.angularVelocity = Vector3.zero; // 회전 제어
        }
    }
    void FixedUpdate() 
    {
        FreezeVelocity();
    }

   void Update()
   {
        // nav.SetDestination(target.position);

         if(isChase) // 추적을 결정하는 bool 변수 사용
             nav.SetDestination(target.position);
   }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Bullet"){ // 원거리 공격 태그
            Bullets bullet = other.GetComponent<Bullets>();
            if(breakObject.isDestroyed) // BreakObject의 활성화 여부 확인
            {
                curHealth -= bullet.damage;

                Vector3 reactVec = transform.position - other.transform.position;

                // 피격 시 총알이 사라지도록 설정
                Destroy(other.gameObject);

                StartCoroutine(OnDamage(reactVec)); // 피격 코루틴 설정

                Debug.Log("Enemy 피격!");
            }
            else
            {
                Debug.Log("BreakObject가 아직 활성화 되지 않았습니다.");
            }
        }
   }

   IEnumerator OnDamage(Vector3 reactVec) // 피격 로직을 담을 코루틴 생성
   {
        //mat.color = Color.red; // 피격 시 색상을 Red 로 설정

        yield return new WaitForSeconds(0.1f); // 피격 딜레이 설정

        if(curHealth > 0) {
            // mat.color = Color.white;
        }
        else {
            // mat.color = Color.grey;
            gameObject.layer = 13; // Enemy의 죽은 상태인 'EnemyDead' 레이어로 변경

            // 처치 시 넉백(사망 리액션) 발생
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            isChase = false; // 죽을 시 추적 중지
            nav.enabled = false; // 사망 리액션 유지를 위한 Nav Agent 비활성화
            anim.SetTrigger("doDie"); // 죽는 시점 플래그 설정

            Destroy(gameObject, 4); // 처치 시 4초 뒤 삭제
        }
   }
}
