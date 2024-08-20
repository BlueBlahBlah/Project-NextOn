using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor2 : MonoBehaviour
{
    private Vector3 closedPosition;  // ���� ���� ��ġ
    private Vector3 openPosition;    // ���� ���� �ִ� ��ġ
    public float duration = 3.0f;                           // ���� ������ �� �ɸ��� �ð�

    private void Start()
    {
        openPosition = this.transform.position;
        closedPosition = this.transform.position - new Vector3(0, openPosition.y, 0);
        // ���� �ݴ� �ڷ�ƾ ����
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            // ���� �ε巴�� �ϰ���Ű�� ���� Lerp ���
            transform.position = Vector3.Lerp(startPosition, closedPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        // ��Ȯ�ϰ� ���� ���� ��ġ�� ����
        transform.position = closedPosition;
    }
}
