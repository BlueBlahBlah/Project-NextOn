using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // 회전 속도 (도/초)

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}