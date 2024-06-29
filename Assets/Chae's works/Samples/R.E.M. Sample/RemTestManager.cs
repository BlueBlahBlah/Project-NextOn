using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemTestManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static RemTestManager instance;

    // 예시 변수 - 플레이어의 체력
    public int hp;
    public bool isNear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 기타 관련 메서드들을 추가할 수 있음
}