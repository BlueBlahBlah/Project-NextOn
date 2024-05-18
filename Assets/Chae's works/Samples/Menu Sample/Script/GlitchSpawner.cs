using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchSpawner : MonoBehaviour
{
    public GameObject glitchPrefab;  // GlitchEffect 프리팹
    public RectTransform canvasRect;  // Canvas의 RectTransform
    public float spawnInterval = 0.5f;  // 스폰 간격
    public Transform Container;

    void Start()
    {
        if (canvasRect == null)
            canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // 일정 간격으로 GlitchEffect를 스폰
        InvokeRepeating("SpawnGlitch", 0f, spawnInterval);
    }

    void SpawnGlitch()
    {
        bool isVertical = Random.value > 0.5f;
        GameObject glitch = Instantiate(glitchPrefab, Container);

        // 초기 위치 설정
        RectTransform glitchRect = glitch.GetComponent<RectTransform>();
        if (isVertical)
        {
            float x = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
            glitchRect.anchoredPosition = new Vector2(x, canvasRect.rect.height / 2 + glitchRect.sizeDelta.y);
        }
        else
        {
            float y = Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2);
            if (Random.value > 0.5f)
            {
                glitchRect.anchoredPosition = new Vector2(canvasRect.rect.width / 2 + glitchRect.sizeDelta.x, y);
            }
            else
            {
                glitchRect.anchoredPosition = new Vector2(-canvasRect.rect.width / 2 - glitchRect.sizeDelta.x, y);
                glitch.GetComponent<GlitchEffect>().speed = -glitch.GetComponent<GlitchEffect>().speed; // 방향 반대로
            }
        }

        // GlitchEffect 초기화
        glitch.GetComponent<GlitchEffect>().Initialize(isVertical);
    }
}

