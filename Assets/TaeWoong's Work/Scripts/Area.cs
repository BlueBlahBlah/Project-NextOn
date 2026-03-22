using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public ParticleSystem enterParticle; // 진입 시 활성화될 파티클

    public bool isDestroyed = false; // 파괴 여부를 나타내는 플래그

    public float rotationSpeed = 50f; // 회전 속도

    void Start()
    {
        enterParticle.Stop(); // 초기에는 파티클을 비활성화
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")) // Player가 영역에 진입했는지 확인
        {
            enterParticle.Play(); // 파티클 활성화
            isDestroyed = true; // 파괴 플래그 활성화
        }    
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player")) // Player가 영역을 떠났는지 확인
        {
            enterParticle.Stop(); // 파티클 비활성화
            isDestroyed = false; // 파괴 플래그 비활성화
        }
    }

    void Update()
    {
        if(isDestroyed)
        {
            // 오브젝트 회전 효과 적용
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
