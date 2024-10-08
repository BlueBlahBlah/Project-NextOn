using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스 사용

public class PlayerGetKeyArea : MonoBehaviour
{
    public Image progressBar; // UI의 Progress Bar
    public float fillDuration = 5f; // Bar가 차는 시간
    private float fillTimer = 0f;
    private bool isPlayerInside = false;

    public GameObject keyEffectPrefab; // 열쇠 이펙트 프리팹
    public bool HasKey = false; // 열쇠를 얻었는지 여부를 나타내는 플래그

    void Update()
    {
        if (isPlayerInside)
        {
            fillTimer += Time.deltaTime;
            float fillAmount = fillTimer / fillDuration;
            progressBar.fillAmount = fillAmount;

            if (fillAmount >= 1f)
            {
                TriggerGetKey();
            }
        }
    }

    private void TriggerGetKey()
    {
        if (!HasKey) // 이미 키를 얻었다면 중복 실행 방지
        {
            HasKey = true; // 열쇠를 얻었다고 플래그 설정
            Debug.Log("열쇠를 얻었습니다!"); // 로그 출력

            // 이펙트 생성
            Instantiate(keyEffectPrefab, transform.position, Quaternion.identity); // 영역의 위치에서 이펙트 생성
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            isPlayerInside = true;
            fillTimer = 0f; // 다시 초기화
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 나갔을 때
        {
            isPlayerInside = false;
            fillTimer = 0f; // 초기화
            progressBar.fillAmount = 0; // Bar 초기화
        }
    }
}
