using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
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
    
    void Start()
    {
        // 초기 위치 저장
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        RollBtn.onClick.AddListener(OnRollButtonClick);         //구르기버튼
    }

    void Update()
    {
        // 현재 위치와 이전 위치 비교
        if (transform.position != lastPosition)
        {
            walking = true;

            // 이동 방향 설정
            Vector3 moveDirection = (transform.position - lastPosition).normalized;

            // 이동 방향을 기준으로 앞, 뒤, 오른쪽, 왼쪽 여부 판단
            float angle = Vector3.SignedAngle(moveDirection, transform.forward, Vector3.up);
            Debug.LogError(angle);
            if (angle > 45f && angle < 135f)
            {
                isMovingLeft = true;
                isMovingRight = false;
                isMovingForward = false;
                isMovingBackward = false;
                
            }
            else if (angle < -45f && angle > -135f)
            {
                isMovingRight = true;
                isMovingLeft = false;
                isMovingForward = false;
                isMovingBackward = false;
            }
            else if(angle > 70 || angle < -70)
            {
                isMovingLeft = false;
                isMovingRight = false;
                if (Vector3.Dot(moveDirection, transform.forward) > 0)      //안쓰이는 코드인듯? forward는 아래 else에
                {
                    isMovingForward = true;
                    isMovingBackward = false;
                }
                else
                {
                    isMovingForward = false;
                    isMovingBackward = true;
                }
            }
            else
            {
                isMovingForward = true;
                isMovingBackward = false;
                isMovingLeft = false;
                isMovingRight = false;
            }
            // 움직임 처리
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // 회전 처리
            //Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 100f);

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

    private void reloadDone()
    {
        Anim.SetBool("reload", false);
        Rifle rifle = GameObject.FindObjectOfType<Rifle>();
        Shotgun shotgun = GameObject.FindObjectOfType<Shotgun>();
        Sniper sniper = GameObject.FindObjectOfType<Sniper>();
        GrenadeLauncher grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
        MachineGun machineGun = GameObject.FindObjectOfType<MachineGun>();
        FireGun fireGun = GameObject.FindObjectOfType<FireGun>();
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
            if (fireGun.maxBulletCount >= 50)
            {
                fireGun.maxBulletCount -= 50;         //탄 잔량 50 감소
                fireGun.bulletCount += 50; // 50발 추가
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
}
