using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public float duration = 2.0f; // 회전이 완료될 시간(초)
    private float elapsedTime = 0.0f;
    private Quaternion startRotation;
    private Quaternion endRotation;
    
    void Start()
    {
        startRotation = this.transform.localRotation;
        if (this.transform.parent.transform.rotation.y != 0)
        {
            endRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            endRotation = Quaternion.Euler(0, 90, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
        // 경과 시간을 증가시킴
        elapsedTime += Time.deltaTime;

        // 경과된 시간 비율에 따라 회전 값을 Lerp로 계산
        float t = Mathf.Clamp01(elapsedTime / duration); // 0에서 1 사이의 값을 얻음
        transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
    }
}
