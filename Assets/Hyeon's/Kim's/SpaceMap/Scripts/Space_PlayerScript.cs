using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Space_PlayerScript : MonoBehaviour
{
    public float moveSpeed = 0f;
    public bool walking;
    private Vector3 lastPosition;
    Animator Anim;
    public Button RollBtn;

    private bool temporary_death;           //임시 죽음 스위치 - Update 제어
    private bool stop_update;               //update 제어


    void Start()
    {
        // 초기 위치 저장
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        //RollBtn.onClick.AddListener(OnRollButtonClick);         //구르기버튼
        temporary_death = false;
        stop_update = false;
    }

    void Update()
    {
        if (stop_update == false)
        {
            //transform.position = new Vector3(0, 0, 0);
            if (PlayerManager.Instance.Health <= 0)        //체력이 다 닳은 경우
            {
                GetComponentInParent<NewPlayer_Locomotion>().enabled = false;
                Anim.applyRootMotion = true;
                Anim.SetTrigger("Death");
                stop_update = true;
            }
            else
            {
                if (temporary_death == false)
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




            if (transform.position.y < -10)
            {
                this.transform.position = new Vector3(0, 0, 43);
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
            PlayerSoundManager.Instance.revive_Sound();
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

    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");        //애니메이션 트리거

    }


}
