using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("라이플 재장전");
            rifle.bulletCount += 30; // 30발 추가
            rifle.nowReloading = false; // 이제 장전 끝
        }
        if (shotgun != null && shotgun.gameObject.activeSelf)
        {
            Debug.Log("샷건 재장전");
            shotgun.bulletCount += 30; // 30발 추가
            shotgun.nowReloading = false; // 이제 장전 끝
        }
        if (sniper != null && sniper.gameObject.activeSelf)
        {
            Debug.Log("저격총 재장전");
            sniper.bulletCount += 1; // 30발 추가
            sniper.nowReloading = false; // 이제 장전 끝
        }
        if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
        {
            Debug.Log("유탄발사기 재장전");
            grenadeLauncher.bulletCount += 30; // 30발 추가
            grenadeLauncher.nowReloading = false; // 이제 장전 끝
        }
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            Debug.Log("기관총 재장전");
            machineGun.bulletCount += 100; // 100발 추가
            machineGun.nowReloading = false; // 이제 장전 끝
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            Debug.Log("화염방사기 재장전");
            fireGun.bulletCount += 100; // 100발 추가
            fireGun.nowReloading = false; // 이제 장전 끝
        }
        
    }
    
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }
}
