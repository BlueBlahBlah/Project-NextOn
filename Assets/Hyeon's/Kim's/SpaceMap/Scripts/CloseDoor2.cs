using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor2 : MonoBehaviour
{
    private Vector3 closedPosition;  // 문이 닫힐 위치
    private Vector3 openPosition;    // 문이 열려 있는 위치
    public float duration = 3.0f;                           // 문이 닫히는 데 걸리는 시간

    private void Start()
    {
        openPosition = this.transform.position;
        closedPosition = this.transform.position - new Vector3(0, openPosition.y, 0);
        // 문을 닫는 코루틴 시작
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            // 문을 부드럽게 하강시키기 위해 Lerp 사용
            transform.position = Vector3.Lerp(startPosition, closedPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 정확하게 문이 닫힌 위치로 설정
        transform.position = closedPosition;
    }
}
