using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Meteor : MonoBehaviour
{
    public int Damage;
    [SerializeField] private BoxCollider AttackArea; // 공격 범위 콜라이더
    [SerializeField] private bool isPlayerInside = false; // 플레이어 진입 여부
    public float damageInterval = 1.0f; // 데미지를 주는 간격 (초)
    public float castDuration = 1.0f; // 공격 영역이 활성화되기까지 준비 시간
    public float activeDuration = 2.0f; // 공격 영역이 활성화되는 시간
    public GameObject castEffect; // 캐스팅 효과
    public GameObject Magic; // 마법 공격
    private Coroutine damageCoroutine; // 데미지 처리를 위한 코루틴 참조

    // Start is called before the first frame update
    void Start()
    {
        // 공격 영역 비활성화 후 일정 시간이 지나면 활성화
        AttackArea.enabled = false; // 공격 영역 비활성화
        StartCoroutine(ActivateAttackArea());
        // MapSoundManager.Instance.Summon_Meteor_Sound();
        SoundManager.instance.PlayEffectSound("보스메테오");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            isPlayerInside = true; 
            Debug.Log("[태웅 디버깅] 메테오 패턴 시작 DMG : " + Damage); 
            // 플레이어에게 데미지를 주는 코루틴 시작
            if (damageCoroutine == null && AttackArea.enabled)
            {
                damageCoroutine = StartCoroutine(DealDamageToPlayer());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더 안에 있을 때
        {
            isPlayerInside = true; // 플레이어가 여전히 안에 있으므로 상태 유지
            // AttackArea가 활성화된 경우에만 데미지 코루틴 시작
            if (damageCoroutine == null && AttackArea.enabled)
            {
                damageCoroutine = StartCoroutine(DealDamageToPlayer());
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 나갔을 때
        {
            isPlayerInside = false; 
            Debug.Log("[태웅 디버깅] 보스 패턴 중단 "); 
            // 플레이어가 나가면 데미지 코루틴 중지
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageToPlayer()
    {
        while (isPlayerInside && AttackArea.enabled) // 플레이어가 AttackArea에 있을 동안 반복
        {
            // AttackArea의 콜라이더 안에 있는 모든 플레이어를 대상으로 데미지 처리
            Collider[] hitColliders = Physics.OverlapBox(AttackArea.bounds.center, AttackArea.bounds.extents, AttackArea.transform.rotation);

            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("Player"))
                {
                    PlayerManager.Instance.Health -= Damage; // 플레이어 데미지 처리
                    Debug.Log("[태웅 디버깅] 보스(매테오) -> 플레이어 DMG : " + Damage); // 데미지 로그 출력
                }
            }

            yield return new WaitForSeconds(damageInterval); // 지정한 간격만큼 대기
        }
    }

    private IEnumerator ActivateAttackArea()
    {
        // 캐스팅 효과 활성화
        castEffect.SetActive(true); // 캐스팅 효과 활성화

        // 일정 시간 후 마법 오브젝트 활성화
        yield return new WaitForSeconds(castDuration); // 캐스팅 효과 대기 시간
        Magic.SetActive(true); // 마법 오브젝트 활성화

        // 공격 영역 활성화
        yield return new WaitForSeconds(0.5f); // 데미지 효과 대기 시간
        AttackArea.enabled = true; // 공격 영역 활성화
        Debug.Log("[태웅 디버깅] AttackArea 활성화됨"); // 디버깅 로그 추가
        yield return new WaitForSeconds(activeDuration); // 활성화 시간 대기
        AttackArea.enabled = false; // 공격 영역 비활성화
        Debug.Log("[태웅 디버깅] AttackArea 비활성화됨"); // 디버깅 로그 추가

        // 캐스팅 효과 비활성화
        castEffect.SetActive(false); // 캐스팅 효과 비활성화

        // Boss_Meteor 오브젝트를 5초 후에 삭제
        yield return new WaitForSeconds(5.0f); // 5초 대기
        Destroy(gameObject); // Boss_Meteor 오브젝트 삭제
    }
}
