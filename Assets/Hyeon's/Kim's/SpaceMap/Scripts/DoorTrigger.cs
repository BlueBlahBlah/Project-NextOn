using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Vector3 m_Position;
    private Coroutine m_ReturnCoroutine;
    private void Start()
    {
        m_Position = transform.position;
        Debug.Log("Hello start");

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) Debug.Log("help");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(this.transform.position.y > -5)
            transform.Translate(Vector3.down * 1);
            Debug.Log("Hello Keep going");


            // �浹 ���� �� ��ġ ���͸� ����
            if (m_ReturnCoroutine != null)
            {
                StopCoroutine(m_ReturnCoroutine);
                m_ReturnCoroutine = null;
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // ��ġ ���� �ڷ�ƾ ����
            if (m_ReturnCoroutine == null)
            {
                m_ReturnCoroutine = StartCoroutine(ReturnToPosition());
            }
        }
    }
    private IEnumerator ReturnToPosition()
    {
        while (transform.position.y < m_Position.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime); // �� �����Ӹ��� õõ�� �̵�
            yield return null; // ���� �����ӱ��� ���
        }

        // ��ġ�� �ʱ� ��ġ�� �����ϸ� ��Ȯ�� �ʱ� ��ġ�� ����
        transform.position = new Vector3(transform.position.x, m_Position.y, transform.position.z);
        m_ReturnCoroutine = null;
    }
}
