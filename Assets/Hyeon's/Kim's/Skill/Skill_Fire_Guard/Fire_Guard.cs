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
        // ���� ������Ʈ�� ������ �ִ� ��� �ݶ��̴��� ã���ϴ�.
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {

        }
    }
}
