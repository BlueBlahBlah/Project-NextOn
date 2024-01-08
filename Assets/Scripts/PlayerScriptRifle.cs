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
        GameObject.Find("SM_Wep_Rifle_Assault_01").GetComponent<Rifle>().bulletCount += 30; //30발 추가
        GameObject.Find("SM_Wep_Rifle_Assault_01").GetComponent<Rifle>().nowReloading = false; //이제 장전 끝
    }
    
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }
}
