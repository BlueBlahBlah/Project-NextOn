using System.Collections;
using UnityEngine;

public class FlockVortex : MonoBehaviour
{
    public float scaleFactor = 1.5f; // ?¬ê¸° ë³€??ë¹„ìœ¨
    public float growDuration = 5.0f; // ?¬ê¸° ì»¤ì???? ë‹ˆë©”ì´???œê°„
    public float shrinkDuration = 3.0f; // ?¬ê¸° ?‘ì•„ì§€??? ë‹ˆë©”ì´???œê°„

    private Vector3 initialScale; // ì´ˆê¸° ?¬ê¸°

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

        // ?€ê¸?        yield return new WaitForSeconds(3.0f);

        // 3ì´??™ì•ˆ ?¬ê¸°ê°€ 0.5ë¡??‘ì•„ì§€?„ë¡
        yield return StartCoroutine(ScaleOverTime(initialScale, shrinkDuration));

        // ?¬ê¸°ë¥?ì´ˆê¸° ?¬ê¸°ë¡??˜ëŒë¦?        transform.localScale = initialScale;

        // ?¤ìŒ Vortex ?¸ì¶œ
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
