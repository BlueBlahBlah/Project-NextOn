using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
   public int maxHealth; // 최대 체력
   public int currentHealth; // 현재 체력

   Rigidbody rigid;
   BoxCollider boxCollider;
   
   public ParticleSystem hitParticle; // 히트 시 발생할 파티클
   public ParticleSystem destroyParticle; // 파괴 시 발생할 파티클

   // 파괴 여부를 외부에서도 확인할 수 있도록 public으로 변경
   public bool isDestroyed = false; // 파괴 여부
   public float rotationSpeed = 50f; // 회전 속도
   //Material mat;

   void Awake() 
   {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>(); 
        currentHealth = maxHealth; // 최대 체력으로 초기화
   }
 
   void OnTriggerEnter(Collider other) 
   {
        // 태그 비교 조건 작성
        if(other.tag == "Bullet")
    {
        Bullets bullet = other.GetComponent<Bullets>();

        if (bullet != null) // Bullet 컴포넌트가 있을 때만 로직 진행
        {
            currentHealth -= bullet.damage;
            StartCoroutine(OnDamage());
            Debug.Log("Range : " + currentHealth);
        }
    }
   }

    // 피격 로직을 담을 코루틴 작성
   IEnumerator OnDamage()
   {
        hitParticle.Play(); // 피격 시 파티클 발생

        yield return new WaitForSeconds(0.1f);

        if(currentHealth <= 0 && !isDestroyed)
        {
            isDestroyed = true; // 파괴 상태로 설정
            destroyParticle.Play(); // 파괴 파티클 재생
            rigid.isKinematic = false; // Rigidbody의 isKinematic을 false로 설정하여 물리 효과 적용
            boxCollider.isTrigger = false; // 더 이상 트리거 이벤트가 발생하지 않도록 설정
        }
   }

   void Update() 
   {
        if(isDestroyed)
        {
            // Rotate 함수로 회전효과 
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        } 
   }
}
