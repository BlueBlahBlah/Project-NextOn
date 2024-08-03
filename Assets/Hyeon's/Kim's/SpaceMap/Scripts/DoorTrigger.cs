using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Vector3 m_Position;
    public bool m_IsGrounded;

    private GameObject Door;
    private Coroutine m_ReturnCoroutine;
    private MeshCollider[] ColliderS;
    private void Start()
    {
        Door = gameObject.transform.GetChild(0).gameObject;
        ColliderS = gameObject.GetComponents<MeshCollider>();
        m_Position = Door.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OpenDoor());

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
    IEnumerator ReturnToPosition()
    {
        while (Door.transform.position.y < m_Position.y)
        {
            Door.transform.Translate(Vector3.up * Time.deltaTime); // 매 프레임마다 천천히 이동
            yield return null; // 다음 프레임까지 대기
        }

        // 위치가 초기 위치에 도달하면 정확히 초기 위치로 설정
        transform.position = new Vector3(transform.position.x, m_Position.y, transform.position.z);
        m_ReturnCoroutine = null;
        EnableColliders(true);
    }

    IEnumerator OpenDoor()
    {
        while (Door.transform.position.y > -5) 
        {
            Door.transform.Translate(Vector3.down * Time.deltaTime * 1.5f);
            EnableColliders(false);
            yield return null;
        }
    }
    private void EnableColliders(bool enabled)
    {
        foreach (var collider in ColliderS)
        {
            collider.enabled = enabled;
        }
    }
}
