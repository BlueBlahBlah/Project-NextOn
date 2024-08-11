using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveTest : MonoBehaviour
{
    public float moveDistance = 1.0f;  // �� ���� ����
    public float speed = 1.0f;  // �̵� �ӵ�

    private Vector3 startPosition;  // ���� ���� ���� ��ġ
    private int sideCount = 0;  // ���� �׸� ���� ��

    void Start()
    {
        startPosition = transform.position;  // �ʱ� ��ġ ����
    }

    void Update()
    {
        // ���� ��ġ���� ���� ��ġ������ �Ÿ� ���
        float distance = Vector3.Distance(transform.position, startPosition);

        // �̵� ������ ȸ�� ������ Ȯ��
        if (distance < moveDistance)
        {
            // ����
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // �������� 90�� ȸ��
            transform.Rotate(Vector3.up, 90.0f);
            sideCount++;

            // ���ο� ���� ���� ��ġ ����
            startPosition = transform.position;

            
        }
    }
}
