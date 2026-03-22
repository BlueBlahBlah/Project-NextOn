using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 따라갈 목표와 위치 오프셋을 Public 변수로 선언
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // 타겟 위치 설정
    }
}
