using System.Collections;
using UnityEngine;

public class FlockVortex : MonoBehaviour
{
    public float scaleFactor = 1.5f; // 크기 변화 비율
    public float growDuration = 5.0f; // 크기 커지는 애니메이션 시간
    public float shrinkDuration = 3.0f; // 크기 작아지는 애니메이션 시간

    private Vector3 initialScale; // 초기 크기

    void Start()
    {
        initialScale = transform.localScale;
        Invoke("vortex", Random.Range(10f, 20f));
    }

    void vortex()
    {
        StartCoroutine(VortexSequence());
    }

    private IEnumerator VortexSequence()
    {
        yield return StartCoroutine(ScaleOverTime(initialScale * scaleFactor, growDuration));

        // 대기
        yield return new WaitForSeconds(3.0f);

        // 3초 동안 크기가 0.5로 작아지도록
        yield return StartCoroutine(ScaleOverTime(initialScale, shrinkDuration));

        // 크기를 초기 크기로 되돌림
        transform.localScale = initialScale;

        // 다음 Vortex 호출
        Invoke("vortex", Random.Range(10f, 20f));
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}