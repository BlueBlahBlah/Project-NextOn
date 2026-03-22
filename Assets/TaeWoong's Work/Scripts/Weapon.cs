using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기 타입, 데미지, 공격 속도, 범위 효과 변수 생성
    public enum Type { Melee, Range };
    public Type type; // 공격 타입
    public int damage; // 데미지
    public float rate; // 공격 속도
    public BoxCollider meleeArea; // 공격 범위 -> BoxCollider 이용 시 플레이어와의 물리적 충돌을 막기 위해 isTrigger 체크 (Trigger 용도로 사용)
    public TrailRenderer trailEffect; // 공격 이펙트
    public Transform bulletPos; // prefab을 생성할 위치
    public GameObject bullet; // prefab을 저장할 변수
    public Transform bulletCasePos; // prefab을 생성할 위치
    public GameObject bulletCase; // prefab을 저장할 변수

    // 코루틴 함수 : 메인 루틴 + 코루틴 (동시 실행)
    // Use() 메인 루틴 -> Swing() 코루틴 (Co-op)
    public void Use()
    {
        // 근접 공격
        if(type == Type.Melee)
        {
            // StartCoroutine() : 코루틴 실행 함수 
            // StopCoroutine() : 코루틴 정지 함수
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }
    
    // IEnumerator : 열거형 함수 클래스
    IEnumerator Swing()
    {
        // yield : 결과를 전달하는 키워드
        // yield 키워드를 여러 개 사용하여 시간차 로직 작성 가능
        // yield break 를 통해 코루틴 탈출 가능

        // TrailRenderer와 BocCollider를 시간차로 활성화 컨트롤 

        yield return new WaitForSeconds(0.1f); // WaitForSeconds() : 주어진 수치만큼 기다리는 함수

        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f); 

        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f); 

        trailEffect.enabled = false;

    }

    IEnumerator Shot()
    {
        // #1. 총알 발사
        // Instantiate() 함수로 총알을 인스턴스화 하기
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50; // 인스턴스화된 총알에 가속도 부여

        yield return null;

        // #2. 탄피 배출
        // Instantiate() 함수로 탄피를 인스턴스화 하기
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
