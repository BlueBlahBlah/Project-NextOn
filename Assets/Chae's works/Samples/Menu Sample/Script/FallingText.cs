using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FallingText : MonoBehaviour
{
    public TextMeshProUGUI fallingText; // 드래그 앤 드롭으로 텍스트 UI를 연결하세요.
    public float fallSpeed = 200f; // 텍스트가 떨어지는 속도.
    public float destroyTime = 3f; // 텍스트가 화면에서 사라지는 시간.

    public RectTransform canvasRect;

    public int alpha = 1;

    void Start()
    {
        // Canvas RectTransform을 찾아 설정
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // 각 구역의 시작과 끝 좌표 계산
        float startX;
        float endX;

        // alpha가 2일 때 특수한 규칙 적용
        if (alpha == 2)
        {
            // 2번째, 3번째, 5번째 구역에서 스폰되도록 설정
            if (Random.Range(0, 3) == 0)
            {
                startX = -960 + (384 * 1);
                endX = startX + 384;
            }
            else if (Random.Range(0, 2) == 0)
            {
                startX = -960 + (384 * 2);
                endX = startX + 384;
            }
            else
            {
                startX = -960 + (384 * 4);
                endX = startX + 384;
            }
        }
        else
        {
            // 일반적인 구역 설정
            startX = -960 + (384 * (alpha - 1));
            endX = startX + 384;
        }

        // 랜덤한 X 좌표 설정
        float randomX = Random.Range(startX, endX);

        // Canvas의 중앙 좌표를 계산
        Vector2 canvasCenter = new Vector2(canvasRect.rect.width / 2, canvasRect.rect.height / 2);

        // 초기 위치 설정 (랜덤한 x 위치에 Y 좌표는 캔버스의 상단으로 설정)
        fallingText.rectTransform.anchoredPosition = new Vector2(randomX, canvasCenter.y);

        // 일정 시간 후 오브젝트를 파괴
        Destroy(gameObject, destroyTime);
    }


    void Update()
    {
        // 텍스트가 아래로 떨어지도록 함
        fallingText.rectTransform.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);
    }
}

