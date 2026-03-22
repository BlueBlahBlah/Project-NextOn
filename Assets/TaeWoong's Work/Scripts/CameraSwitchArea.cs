using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchArea : MonoBehaviour
{
    public delegate void ActivationHandler();
    public event ActivationHandler OnActivated;
    public bool isActivateScript = false; // 스크립트 실행 여부
    public int ScriptOrder; // 스크립트 순서
    public int ScriptNum; // 스크립트 고유 번호
    public Camera newCamera; // 새로운 카메라
    public Camera currentCamera; // 현재 카메라

    private void Start()
    {
        currentCamera = Camera.main; // 기본 카메라를 현재 카메라로 설정
        if (currentCamera == null)
        {
            Debug.LogError("No main camera found!"); // 카메라가 없을 경우 에러 로그 출력
        }
        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(false); // 새로운 카메라는 비활성화
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) // 플레이어가 영역에 들어왔는지 확인
        {
            Debug.Log("Player has entered the area."); // 로그 출력

            if (!isActivateScript) // 처음으로 활성화될 때만
            {
                isActivateScript = true; // 활성화 상태 설정
                OnActivated?.Invoke(); // 이벤트 발생
            }

            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false); // 현재 카메라 비활성화
                // currentCamera.GetComponent<AudioListener>().enabled = false; // 오디오 리스너 비활성화
            }

            if (newCamera != null)
            {
                newCamera.gameObject.SetActive(true); // 새로운 카메라 활성화
                // newCamera.GetComponent<AudioListener>().enabled = true; // 오디오 리스너 활성화
            }
        } 
        else
        {
            Debug.Log("An object entered the area that is not the player: " + other.name); // 다른 객체 로그 출력
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player")) // 플레이어가 영역을 나갔는지 확인
        {
            Debug.Log("Player has exited the area."); // 로그 출력

            isActivateScript = false;

            if (newCamera != null)
            {
                newCamera.gameObject.SetActive(false); // 새로운 카메라 비활성화
                // newCamera.GetComponent<AudioListener>().enabled = false; // 오디오 리스너 비활성화
            }

            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(true); // 원래 카메라 활성화
                // currentCamera.GetComponent<AudioListener>().enabled = true; // 오디오 리스너 활성화
            }
        }
    }
}
