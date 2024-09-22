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
    [SerializeField] private Rifle rifle;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private Sniper sniper;
    [SerializeField] private GrenadeLauncher grenadeLauncher;
    [SerializeField] private MachineGun machineGun;
    [SerializeField] private FireGun fireGun;

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
                 // 현재 위치와 이전 위치 비교
                if (transform.position != lastPosition)
                {
                    walking = true;

                    // 이동 방향 설정
                    Vector3 moveDirection = (transform.position - lastPosition).normalized;

                    // 이동 방향을 기준으로 앞, 뒤, 오른쪽, 왼쪽 여부 판단
                    float angle = Vector3.SignedAngle(moveDirection, transform.forward, Vector3.up);
                    //Debug.LogError(angle);
                    if (angle > 45f && angle < 135f)
                    {
                        isMovingLeft = true;
                        isMovingRight = false;
                        isMovingForward = false;
                        isMovingBackward = false;
                        playerMovingScript.walkSpeed = 5;

                    }
                    else if (angle < -45f && angle > -135f)
                    {
                        isMovingRight = true;
                        isMovingLeft = false;
                        isMovingForward = false;
                        isMovingBackward = false;
                        playerMovingScript.walkSpeed = 5;
                    }
                    else if(angle > 70 || angle < -70)
                    {
                        isMovingLeft = false;
                        isMovingRight = false;
                        if (Vector3.Dot(moveDirection, transform.forward) > 0)      //안쓰이는 코드인듯? forward는 아래 else에
                        {
                            isMovingForward = true;
                            isMovingBackward = false;
                            playerMovingScript.walkSpeed = 5;
                        }
                        else
                        {
                            isMovingForward = false;
                            isMovingBackward = true;
                            playerMovingScript.walkSpeed = 5;
                        }
                    }
                    else
                    {
                        isMovingForward = true;
                        isMovingBackward = false;
                        isMovingLeft = false;
                        isMovingRight = false;
                        playerMovingScript.walkSpeed = 5;
                    }
                    // 움직임 처리
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                    // 회전 처리
                    Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 100f);

                    // 현재 위치를 이전 위치로 업데이트
                    lastPosition = transform.position;
                    
                }
                else
                {
                    walking = false;
                    isMovingForward = false;
                    isMovingBackward = false;
                    isMovingRight = false;
                    isMovingLeft = false;
                }
                Anim.SetBool("walk", walking);
                Anim.SetBool("Front", isMovingForward);
                Anim.SetBool("Back", isMovingBackward);
                Anim.SetBool("Left", isMovingLeft);
                Anim.SetBool("Right", isMovingRight);
                

                if (reloaing == true)       //재장전중이라면
                {
                    Anim.SetBool("reload", true);       //재장전 애니메이션
                    reloaing = false;
                    Invoke("reloadDone",4);      //4초후 재장전 끝
                }
            }
        }
       
       
    }
    
    public void WeaponSynchronization()
    {
        /*rifle = GameObject.FindObjectOfType<Rifle>();
        shotgun = GameObject.FindObjectOfType<Shotgun>();
        sniper = GameObject.FindObjectOfType<Sniper>();
        grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
        machineGun = GameObject.FindObjectOfType<MachineGun>();
        fireGun = GameObject.FindObjectOfType<FireGun>();*/
    }

    private void reloadDone()
    {
        Anim.SetBool("reload", false);
        try
        {
            WeaponSynchronization();
        }
        catch (NullReferenceException e)
        {
            
        }
        if (rifle != null && rifle.gameObject.activeSelf)
        {
            if (rifle.maxBulletCount >= 30)
            {
                rifle.maxBulletCount -= 30;         //탄 잔량 30 감소
                rifle.bulletCount += 30; // 30발 추가
                rifle.nowReloading = false; // 이제 장전 끝
            }
            else
            {
                rifle.bulletCount += rifle.maxBulletCount; //남은 탄 모조리 재장전
                rifle.maxBulletCount = 0;    //잔탄 소모
                rifle.nowReloading = false; // 이제 장전 끝
            }
           
        }
        if (shotgun != null && shotgun.gameObject.activeSelf)
        {
            if (shotgun.maxBulletCount >= 15)
            {
                shotgun.maxBulletCount -= 15;         //탄 잔량 15 감소
                shotgun.bulletCount += 15; // 15발 추가
                shotgun.nowReloading = false; // 이제 장전 끝
            }
            else
            {
                shotgun.bulletCount += shotgun.maxBulletCount; //남은 탄 모조리 재장전
                shotgun.maxBulletCount = 0;    //잔탄 소모
                shotgun.nowReloading = false; // 이제 장전 끝
            }
        }
        if (sniper != null && sniper.gameObject.activeSelf)
        {
            if (sniper.maxBulletCount >= 15)
            {
                sniper.maxBulletCount -= 15;         //탄 잔량 15 감소
                sniper.bulletCount += 15; // 15발 추가
                sniper.nowReloading = false; // 이제 장전 끝
            }
            else
            {
                sniper.bulletCount += sniper.maxBulletCount; //남은 탄 모조리 재장전
                sniper.maxBulletCount = 0;    //잔탄 소모
                sniper.nowReloading = false; // 이제 장전 끝
            }
        }
        if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
        {
            if (grenadeLauncher.maxBulletCount >= 30)
            {
                grenadeLauncher.maxBulletCount -= 30;         //탄 잔량 30 감소
                grenadeLauncher.bulletCount += 30; // 30발 추가
                grenadeLauncher.nowReloading = false; // 이제 장전 끝
            }
            else
            {
                grenadeLauncher.bulletCount += grenadeLauncher.maxBulletCount; //남은 탄 모조리 재장전
                grenadeLauncher.maxBulletCount = 0;    //잔탄 소모
                grenadeLauncher.nowReloading = false; // 이제 장전 끝
            }
        }
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            if (machineGun.maxBulletCount >= 100)
            {
                machineGun.maxBulletCount -= 100;         //탄 잔량 100 감소
                machineGun.bulletCount += 100; // 100발 추가
                machineGun.nowReloading = false; // 이제 장전 끝
            }
            else
            {
                machineGun.bulletCount += machineGun.maxBulletCount; //남은 탄 모조리 재장전
                machineGun.maxBulletCount = 0;    //잔탄 소모
                machineGun.nowReloading = false; // 이제 장전 끝
            }
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            if (fireGun.maxBulletCount >= 100)
            {
                fireGun.maxBulletCount -= 100;         //탄 잔량 50 감소
                fireGun.bulletCount += 100; // 50발 추가
                fireGun.nowReloading = false; // 이제 장전 끝
            }
            else
            {
                fireGun.bulletCount += fireGun.maxBulletCount; //남은 탄 모조리 재장전
                fireGun.maxBulletCount = 0;    //잔탄 소모
                fireGun.nowReloading = false; // 이제 장전 끝
            }
        }
        
    }
    
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }

    //PlayerManager에 현재 탄 잔량 정보를 전달
    public void BulletInfo()
    {
        if (rifle != null && rifle.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = rifle.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = rifle.bulletCount;
            return;
        }
        if (shotgun != null && shotgun.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = shotgun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = shotgun.bulletCount;
            return;
        }
        if (sniper != null && sniper.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = sniper.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = sniper.bulletCount;
            return;
        }
        if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = grenadeLauncher.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = grenadeLauncher.bulletCount;
            return;
        }
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = machineGun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = machineGun.bulletCount;
            return;
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = fireGun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = fireGun.bulletCount;
            return;
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
