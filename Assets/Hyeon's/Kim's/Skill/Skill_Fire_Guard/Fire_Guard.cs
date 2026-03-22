using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Guard : MonoBehaviour
{
    void Start()
    {
        
    }

    private void FindColliders()
    {
        // 현재 오브젝트가 가지고 있는 모든 콜라이더를 찾습니다.
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {

        }
    }
}
