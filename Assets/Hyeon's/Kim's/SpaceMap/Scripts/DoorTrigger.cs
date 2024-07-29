using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Vector3 m_Position;
    public bool m_IsGrounded;

    private GameObject Door;
    private Coroutine m_ReturnCoroutine;
    private BoxCollider boxCollider;
    private void Start()
    {
        Door = this.gameObject.transform.GetChild(0).gameObject;
        boxCollider = this.gameObject.transform.GetComponent<BoxCollider>();
        m_Position = Door.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {

            StartCoroutine("OpenDoor");

            // 충돌 종료 시 위치 복귀를 멈춤
            if (m_ReturnCoroutine != null)
            {
                StopCoroutine(m_ReturnCoroutine);
                m_ReturnCoroutine = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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
            Door.transform.Translate(Vector3.up * Time.deltaTime); // 매 프레임마다 천천히 이동
            yield return null; // 다음 프레임까지 대기
        }

        // 위치가 초기 위치에 도달하면 정확히 초기 위치로 설정
        transform.position = new Vector3(transform.position.x, m_Position.y, transform.position.z);
        m_ReturnCoroutine = null;
        boxCollider.enabled = true;
    }

    IEnumerator OpenDoor()
    {
        while (transform.position.y > -5) 
        {
            Door.transform.Translate(Vector3.down * Time.deltaTime);
        }
        boxCollider.enabled = false;
        yield return null;
    }
}
