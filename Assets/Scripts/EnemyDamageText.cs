using System.Collections;
using UnityEngine;

public class EnemyDamageText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpperText());
        Destroy(gameObject,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpperText()
    {
        float elapsedTime = 0f;
        float duration = 1f; // 1초 동안 진행될 시간

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + Vector3.up; // 현재 위치에서 Y축으로 +1

        while (elapsedTime < duration)
        {
            // 시간에 따라 위치를 보간하여 이동
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 한 프레임 대기
            yield return null;
        }

        // 마지막에 목표 위치로 정확히 위치를 조정
        transform.position = targetPosition;
    }
}