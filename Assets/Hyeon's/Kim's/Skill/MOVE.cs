using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE : MonoBehaviour
{
    public float speed = 1.0f; // �ʴ� �̵� �ӵ�

    void Update()
    {
        float horizontalMovement = speed * Time.deltaTime;

        // ���� �����ӿ��� �̵��� �Ÿ��� ����Ͽ� �̵�
        transform.Translate(horizontalMovement, 0f, 0f);
    }
 
}
