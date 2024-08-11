using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchSpawner : MonoBehaviour
{
    public GameObject glitchPrefab;  // GlitchEffect ������
    public RectTransform canvasRect;  // Canvas�� RectTransform
    public float spawnInterval = 0.5f;  // ���� ����
    public Transform Container;

    void Start()
    {
        if (canvasRect == null)
            canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // ���� �������� GlitchEffect�� ����
        InvokeRepeating("SpawnGlitch", 0f, spawnInterval);
    }

    void SpawnGlitch()
    {
        bool isVertical = Random.value > 0.5f;
        GameObject glitch = Instantiate(glitchPrefab, Container);

        // �ʱ� ��ġ ����
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
                glitch.GetComponent<GlitchEffect>().speed = -glitch.GetComponent<GlitchEffect>().speed; // ���� �ݴ��
            }
        }

        // GlitchEffect �ʱ�ȭ
        glitch.GetComponent<GlitchEffect>().Initialize(isVertical);
    }
}

