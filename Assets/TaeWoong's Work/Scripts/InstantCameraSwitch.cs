using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCameraSwitch : MonoBehaviour
{
    public delegate void ActivationHandler();
    public event ActivationHandler OnActivated;
    public bool isActivateScript = false; // 스크립트 실행 여부
    public int ScriptOrder; // 스크립트 순서
    public int ScriptNum; // 스크립트 고유 번호
    public Camera newCamera; // 새로운 카메라
    private Camera currentCamera; // 현재 카메라
    public GameObject[] objectsToActivate; // 활성화할 오브젝트 배열
    public GameObject[] objectsToDeactivate; // 비활성화할 오브젝트 배열

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
            }

            if (newCamera != null)
            {
                newCamera.gameObject.SetActive(true); // 새로운 카메라 활성화
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

            isActivateScript = false; // 스크립트 해제

            if (newCamera != null)
            {
                newCamera.gameObject.SetActive(false); // 새로운 카메라 비활성화
            }

            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(true); // 원래 카메라 활성화
            }

            // 특정 오브젝트들을 활성화
            ActivateObjects();

            // 특정 오브젝트들을 비활성화
            DeactivateObjects();

            // 현재 오브젝트를 비활성화
            gameObject.SetActive(false);
        }
    }

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true); // 오브젝트 활성화
            }
        }
    }

    private void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false); // 오브젝트 비활성화
            }
        }
    }
}
