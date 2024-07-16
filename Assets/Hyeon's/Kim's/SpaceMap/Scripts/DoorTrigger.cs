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


            // 충돌 종료 시 위치 복귀를 멈춤
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
            // 위치 복귀 코루틴 시작
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
            transform.Translate(Vector3.up * Time.deltaTime); // 매 프레임마다 천천히 이동
            yield return null; // 다음 프레임까지 대기
        }

        // 위치가 초기 위치에 도달하면 정확히 초기 위치로 설정
        transform.position = new Vector3(transform.position.x, m_Position.y, transform.position.z);
        m_ReturnCoroutine = null;
    }
}
