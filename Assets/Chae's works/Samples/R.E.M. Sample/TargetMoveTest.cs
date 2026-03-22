using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveTest : MonoBehaviour
{
    public float moveDistance = 1.0f;  // 한 변의 길이
    public float speed = 1.0f;  // 이동 속도

    private Vector3 startPosition;  // 현재 변의 시작 위치
    private int sideCount = 0;  // 현재 그린 변의 수

    void Start()
    {
        startPosition = transform.position;  // 초기 위치 저장
    }

    void Update()
    {
        // 현재 위치에서 시작 위치까지의 거리 계산
        float distance = Vector3.Distance(transform.position, startPosition);

        // 이동 중인지 회전 중인지 확인
        if (distance < moveDistance)
        {
            // 전진
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // 왼쪽으로 90도 회전
            transform.Rotate(Vector3.up, 90.0f);
            sideCount++;

            // 새로운 변의 시작 위치 저장
            startPosition = transform.position;

            
        }
    }
}
