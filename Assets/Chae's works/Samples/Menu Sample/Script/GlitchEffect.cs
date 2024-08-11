using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    public RectTransform imageTransform;  // �̹����� RectTransform
    public float speed = 200f;  // �̵� �ӵ�
    private Vector2 direction;  // �̵� ����
    private float width;
    private float height;

    private Color[] colors = new Color[]
    {
        new Color(255f / 255f, 0f, 228f / 255f, 150f / 255f),   // ù��° �� (RGB: 255, 0, 228, A: 150)
        new Color(166f / 255f, 0f, 255f / 255f, 150f / 255f),   // �ι�° �� (RGB: 166, 0, 255, A: 150)
        new Color(0f, 255f / 255f, 196f / 255f, 150f / 255f)    // ����° �� (RGB: 0, 255, 196, A: 150)
    };

    public void Initialize(bool isVertical)
    {
        if (imageTransform == null)
            imageTransform = GetComponent<RectTransform>();

        // �̵� ���� ����
        if (isVertical)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Random.value > 0.5f ? Vector2.left : Vector2.right;
        }

        // ũ�� ����
        if (isVertical)
        {
            height = Random.Range(50f, 450f);
            width = Random.Range(5f, 10f);
            imageTransform.sizeDelta = new Vector2(width, height);
        }
        else
        {
            height = Random.Range(5f, 10f);
            width = Random.Range(50f, 450f);
            imageTransform.sizeDelta = new Vector2(width, height);
        }

        // ���� ����
        GetComponent<UnityEngine.UI.Image>().color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        // �̹��� �̵�
        imageTransform.anchoredPosition += direction * speed * Time.deltaTime;

        // ȭ�� ������ ������ ����
        if (imageTransform.anchoredPosition.y < -Screen.height ||
            imageTransform.anchoredPosition.x < -Screen.width ||
            imageTransform.anchoredPosition.x > Screen.width)
        {
            Destroy(gameObject);
        }
    }
}
