using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    public RectTransform imageTransform;  // 이미지의 RectTransform
    public float speed = 200f;  // 이동 속도
    private Vector2 direction;  // 이동 방향
    private float width;
    private float height;

    private Color[] colors = new Color[]
    {
        new Color(255f / 255f, 0f, 228f / 255f, 150f / 255f),   // 첫번째 색 (RGB: 255, 0, 228, A: 150)
        new Color(166f / 255f, 0f, 255f / 255f, 150f / 255f),   // 두번째 색 (RGB: 166, 0, 255, A: 150)
        new Color(0f, 255f / 255f, 196f / 255f, 150f / 255f)    // 세번째 색 (RGB: 0, 255, 196, A: 150)
    };

    public void Initialize(bool isVertical)
    {
        if (imageTransform == null)
            imageTransform = GetComponent<RectTransform>();

        // 이동 방향 설정
        if (isVertical)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Random.value > 0.5f ? Vector2.left : Vector2.right;
        }

        // 크기 설정
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

        // 색상 설정
        GetComponent<UnityEngine.UI.Image>().color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        // 이미지 이동
        imageTransform.anchoredPosition += direction * speed * Time.deltaTime;

        // 화면 밖으로 나가면 삭제
        if (imageTransform.anchoredPosition.y < -Screen.height ||
            imageTransform.anchoredPosition.x < -Screen.width ||
            imageTransform.anchoredPosition.x > Screen.width)
        {
            Destroy(gameObject);
        }
    }
}
