using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerScriptRifle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool walking;
    public bool reloaing;
    private Vector3 lastPosition;
    Animator Anim;
    public Button RollBtn;
    public bool[] NowWeapon;
    
    public bool isMovingForward;
    public bool isMovingBackward;
    public bool isMovingRight;
    public bool isMovingLeft;
    [SerializeField] private CharacterLocomotion playerMovingScript;
    public GunBase currentGun;

    private bool temporary_death;           //임시 죽음 스위치 - Update 제어
    private bool stop_update;               //update 제어
    
    void Start()
    {
        // 초기 위치 저장
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        RollBtn.onClick.AddListener(OnRollButtonClick);         //구르기버튼
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

    void Update()
    {
        if (stop_update == false)
        {
            if (PlayerManager.Instance.Health <= 0) //체력이 다 닳은 경우
            {
                GetComponentInParent<CharacterLocomotion>().enabled = false;
                Anim.applyRootMotion = true;
                Anim.SetTrigger("Death");
                stop_update = true;
            }
            else
            {
                CharacterLocomotion locomotion = GetComponentInParent<CharacterLocomotion>();
                bool isInputActive = locomotion != null && locomotion.MovementMagnitude > 0.05f;

                if (isInputActive)
                {
                    walking = true;

                    // 이동 방향 (조이스틱 입력 기준)
                    Vector2 input = locomotion != null ? locomotion.JoystickInput : Vector2.zero;
                    Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;
                    if (moveDirection == Vector3.zero) moveDirection = transform.forward; // fallback

                    // 이동 방향을 기준으로 앞, 뒤, 오른쪽, 왼쪽 여부 판단
                    float angle = Vector3.SignedAngle(moveDirection, transform.forward, Vector3.up);
                    //Debug.LogError(angle);
                    if (angle > 45f && angle < 135f)
                    {
                        isMovingLeft = true;
                        isMovingRight = false;
                        isMovingForward = false;
                        isMovingBackward = false;
                        if(playerMovingScript != null) playerMovingScript.walkSpeed = 5;

                    }
                    else if (angle < -45f && angle > -135f)
                    {
                        isMovingRight = true;
                        isMovingLeft = false;
                        isMovingForward = false;
                        isMovingBackward = false;
                        if(playerMovingScript != null) playerMovingScript.walkSpeed = 5;
                    }
                    else if(angle > 70 || angle < -70)
                    {
                        isMovingLeft = false;
                        isMovingRight = false;
                        if (Vector3.Dot(moveDirection, transform.forward) > 0)      //안쓰이는 코드인듯? forward는 아래 else에
                        {
                            isMovingForward = true;
                            isMovingBackward = false;
                            if(playerMovingScript != null) playerMovingScript.walkSpeed = 5;
                        }
                        else
                        {
                            isMovingForward = false;
                            isMovingBackward = true;
                            if(playerMovingScript != null) playerMovingScript.walkSpeed = 5;
                        }
                    }
                    else
                    {
                        isMovingForward = true;
                        isMovingBackward = false;
                        isMovingLeft = false;
                        isMovingRight = false;
                        if(playerMovingScript != null) playerMovingScript.walkSpeed = 5;
                    }
                    // 움직임 처리
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                    // 회전 처리
                    if (moveDirection != Vector3.zero) {
                        Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 100f);
                    }
                }
                else
                {
                    walking = false;
                    isMovingForward = false;
                    isMovingBackward = false;
                    isMovingRight = false;
                    isMovingLeft = false;
                }
                
                // 오차 누적 방지를 위해 매 프레임 위치 갱신
                lastPosition = transform.position;

                Anim.SetBool("walk", walking);
                Anim.SetBool("Front", isMovingForward);
                Anim.SetBool("Back", isMovingBackward);
                Anim.SetBool("Left", isMovingLeft);
                Anim.SetBool("Right", isMovingRight);
                

                if (reloaing == true)       //재장전중이라면
                {
                    PlayerSoundManager.Instance.reload_Sound();
                    Anim.SetBool("reload", true);       //재장전 애니메이션
                    reloaing = false;
                    Invoke("reloadDone",4);      //4초후 재장전 끝
                }
            }
        }
       
       
    }
    
    public void WeaponSynchronization()
    {
        // 하위 오브젝트에서 활성화된 GunBase를 찾습니다. (실시간 생성 시에도 대응)
        currentGun = GetComponentInChildren<GunBase>(false);
    }

    private void reloadDone()
    {
        Anim.SetBool("reload", false);
        WeaponSynchronization();

        if (currentGun != null && currentGun.gunData != null)
        {
            int reloadAmount = currentGun.gunData.maxMagazineSize;
            
            if (currentGun.maxBulletCount >= reloadAmount)
            {
                currentGun.maxBulletCount -= reloadAmount;
                currentGun.bulletCount += reloadAmount;
            }
            else
            {
                currentGun.bulletCount += currentGun.maxBulletCount;
                currentGun.maxBulletCount = 0;
            }
            currentGun.nowReloading = false;
        }

        PlayerSoundManager.Instance.reload_Sound_stop();
    }
    
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }

    //PlayerManager에 현재 탄 잔량 정보를 전달
    public void BulletInfo()
    {
        if (currentGun != null)
        {
            PlayerManager.Instance.TotalBullet = currentGun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = currentGun.bulletCount;
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

        // NavMeshAgent가 활성화되어있고 NavMesh에 배치되어있는지 확인
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
        {
            yield break;
        }

        // 밀어내는 동안 NavMeshAgent가 경로를 따라 움직이지 않도록 비활성화
        agent.isStopped = true;

        // pushDuration 동안 밀어내기
        while (elapsedTime < pushDuration)
        {
            // 매 프레임마다 agent 상태 확인
            if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            {
                break;
            }

            agent.Move(pushVelocity * Time.deltaTime); // NavMeshAgent의 Move()로 직접 이동 적용
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 밀어낸 후 NavMeshAgent 재활성화 (agent가 아직 유효한 경우만)
        if (agent != null && agent.enabled)
        {
            agent.isStopped = false;
        }
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
