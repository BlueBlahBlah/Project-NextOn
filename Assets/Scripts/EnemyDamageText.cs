using System.Collections;
using UnityEngine;

public class EnemyDamageText : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(UpperText());
        Destroy(gameObject, 1);
    }

    void Update()
    {
        
    }

    IEnumerator UpperText()
    {
        float elapsedTime = 0f;
        float duration = 1f;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + Vector3.up;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
