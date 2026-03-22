using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE : MonoBehaviour
{
    public float speed = 1.0f; // 초당 이동 속도

    void Update()
    {
        float horizontalMovement = speed * Time.deltaTime;

        // 현재 프레임에서 이동할 거리를 계산하여 이동
        transform.Translate(horizontalMovement, 0f, 0f);
    }
 
}
