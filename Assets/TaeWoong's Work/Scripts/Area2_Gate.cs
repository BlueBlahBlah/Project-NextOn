using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObjectsOnPlayerEnter : MonoBehaviour
{
    public GameObject[] objectsToDeactivate; // 비활성화할 오브젝트 배열
    public GameObject effectPrefab; // 이펙트 프리팹
    public Vector3 effectPosition; // 이펙트가 발생할 고정 위치
    public GameObject objectA; // A 오브젝트
    public GameObject objectB; // B 오브젝트

    public PlayerGetKeyArea playerArea; // PlayerGetKeyArea 스크립트 참조

    private void Start()
    {
        playerArea = FindObjectOfType<PlayerGetKeyArea>();
        UpdateKeyStatus(); // 초기 상태 업데이트
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) // 플레이어가 영역에 들어왔는지 확인
        {
            Debug.Log("Player has entered the area."); // 로그 출력

            // hasKey가 true일 때만 오브젝트 비활성화
            if (playerArea.HasKey)
            {
                // 지정된 오브젝트들을 비활성화
                foreach (GameObject obj in objectsToDeactivate)
                {
                    if (obj != null)
                    {
                        Instantiate(effectPrefab, effectPosition, Quaternion.identity);
                        obj.SetActive(false);
                    }
                }
                // MapSoundManager.Instance.Unlock_Sound();
                SoundManager.instance.PlayEffectSound("Area2_보안해제"); // MapSoundManager와 동일한 메서드 호출
            }
            else
            {
                Debug.Log("Player has no key, objects will not be deactivated."); // 키가 없을 때 로그 출력
            }
        } 
    }

    private void Update()
    {
        UpdateKeyStatus(); // 매 프레임마다 키 상태 업데이트
    }

    private void UpdateKeyStatus()
    {
        if (playerArea != null)
        {
            // Debug.Log("HasKey: " + playerArea.HasKey); // hasKey 값 출력
            if (playerArea.HasKey) // hasKey 속성 사용
            {
                objectA.SetActive(false); // A 비활성화
                objectB.SetActive(true);  // B 활성화
            }
            else
            {
                objectA.SetActive(true);  // A 활성화
                objectB.SetActive(false); // B 비활성화
            }
        }
    }
}
