using System.Collections;
using UnityEngine;

public class FlockVortex : MonoBehaviour
{
    public float scaleFactor = 1.5f; // ?ш린 蹂??鍮꾩쑉
    public float growDuration = 5.0f; // ?ш린 而ㅼ????좊땲硫붿씠???쒓컙
    public float shrinkDuration = 3.0f; // ?ш린 ?묒븘吏???좊땲硫붿씠???쒓컙

    private Vector3 initialScale; // 珥덇린 ?ш린

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

        // ?湲?        yield return new WaitForSeconds(3.0f);

        // 3珥??숈븞 ?ш린媛 0.5濡??묒븘吏?꾨줉
        yield return StartCoroutine(ScaleOverTime(initialScale, shrinkDuration));

        // ?ш린瑜?珥덇린 ?ш린濡??섎룎由?        transform.localScale = initialScale;

        // ?ㅼ쓬 Vortex ?몄텧
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
