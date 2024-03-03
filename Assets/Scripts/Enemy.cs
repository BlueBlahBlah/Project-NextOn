using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; // Nav 관련 클래스는 UnityEngine.AI 네임스페이스 사용

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public bool isChase; // 추적을 결정하는 bool 변수
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav; // Nav Agent를 사용하기 위해서는 Nav Mesh 생성 필수
    // NavMesh : NavAgent가 경로를 그리기 위한 바탕(Mesh)
    Animator anim;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        // Enemy 오브젝트의 자식 컴포넌트에 MeshRender가 존재하기에 Children으로 부터 불러오기
        mat = GetComponentInChildren<MeshRenderer>().material; // Material은 Mesh Render 컴포넌트를 통해서 접근 가능
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Invoke("ChaseStart", 1); // 1초 뒤 추적 개시
    }
   
   void ChaseStart() // 추적을 결정하는 bool 변수 사용을 제어하는 함수
   {
        isChase = true;
        anim.SetBool("isWalk", true);
   }
   void Update()
   {
        // nav.SetDestination(target.position);

         if(isChase) // 추적을 결정하는 bool 변수 사용
             nav.SetDestination(target.position);
   }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Melee"){ // 근접 공격 태그
            // 근접 공격 구현 후 작성
            // isTrigger 체크 확인
            // 후처리 로직 구현 필요
           // StartCoroutine(OnDamage());
        }
        else if(other.tag == "Bullet"){ // 원거리 공격 태그
            // 원거리 공격 구현 후 작성
            // isTrigger 체크 확인
             // 후처리 로직 구현 필요
            //StartCoroutine(OnDamage());
        }
   }

   IEnumerator OnDamage() // 로직을 담을 코루틴 생성
   {
        mat.color = Color.red; // 피격 시 색상을 Red 로 설정
        yield return new WaitForSeconds(0.1f); // 피격 설정

        if(curHealth > 0) {
            mat.color = Color.white;
        }
        else {
            mat.color = Color.grey;
            // isChase = false; // 죽을 시 추적 중지
            // nav.enabled = false; // 사망 리액션 유지를 위한 Nav Agent 비활성화
            // anim.SetTrigger("doDie"); // 죽는 시점 설정
            // 추가적인 layer 설정 필요 (미구현)
            Destroy(gameObject, 4); // 처치 시 4초 뒤 삭제
        }
   }

   public void stopNav()
   {
       nav.Stop();
   }
   public void startNav()
   {
       nav.Resume();
   }
}
