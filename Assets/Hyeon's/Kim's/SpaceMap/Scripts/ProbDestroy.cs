using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbDestroy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Untagged"))
        {
            // �浹�� �߻��ϸ� 3�� �ڿ� ������Ʈ�� ����
            StartCoroutine(DestroyAfterDelay());
        }

    }

    private IEnumerator DestroyAfterDelay()
    {
        // 3�� ���
        yield return new WaitForSeconds(3);

        // ������Ʈ ����
        Destroy(gameObject);
    }
}
