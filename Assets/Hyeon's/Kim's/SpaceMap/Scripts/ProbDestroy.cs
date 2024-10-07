using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbDestroy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Untagged"))
        {
            // 충돌이 발생하면 3초 뒤에 오브젝트를 삭제
            StartCoroutine(DestroyAfterDelay());
        }

    }

    private IEnumerator DestroyAfterDelay()
    {
        // 3초 대기
        yield return new WaitForSeconds(3);

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}
