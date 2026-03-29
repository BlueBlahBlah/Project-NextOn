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
        float duration = 1f; // 1Ï¥??ôÏïà ÏßÑÌñâ???úÍ∞Ñ

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + Vector3.up; // ?ÑÏû¨ ?ÑÏπò?êÏÑú YÏ∂ïÏúºÎ°?+1

        while (elapsedTime < duration)
        {
            // ?úÍ∞Ñ???∞Îùº ?ÑÏπòÎ•?Î≥¥Í∞Ñ?òÏó¨ ?¥Îèô
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            // Í≤ΩÍ≥º ?úÍ∞Ñ ?ÖÎç∞?¥Ìä∏
            elapsedTime += Time.deltaTime;

            // ???ÑÎ†à???ÄÍ∏?            yield return null;
        }

        // ÎßàÏ?ÎßâÏóê Î™©Ìëú ?ÑÏπòÎ°??ïÌôï???ÑÏπòÎ•?Ï°∞Ï†ï
        transform.position = targetPosition;
    }
}
