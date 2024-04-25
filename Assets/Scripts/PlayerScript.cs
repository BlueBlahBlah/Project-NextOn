using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool walking;
    private Vector3 lastPosition;
    Animator Anim;
    public Button RollBtn;

    void Start()
    {
        // 초기 위치 저장
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        //RollBtn.onClick.AddListener(OnRollButtonClick);         //구르기버튼
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
        
        
    }
    
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");        //애니메이션 트리거
        
    }

    
}
